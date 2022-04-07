using System;
using System.Linq;
using System.Text;
using CraftsApi.Application;
using CraftsApi.Auth;
using CraftsApi.DataAccess;
using CraftsApi.Service;
using CraftsApi.Service.Authentication;
using CraftsApi.Service.Background;
using CraftsApi.Service.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CraftsApi
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IJwtManager, JwtManager>();

            // for background service worker
            services.AddSingleton<IPageWorker, PageWorker>();

            services.AddSingleton<IDataAccess>(_ =>
                new DataAccess.DataAccess(
                    Configuration["ConnectionStrings:Default"].ToString(),
                    _webHostEnvironment
                )
            );
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IDataService, DataService>();

            services.AddSingleton<IPageApplication, PageApplication>();
            services.AddSingleton<IUserApplication, UserApplication>();
            services.AddSingleton<IDataApplication, DataApplication>();

            services.AddSingleton<AuthTokenAuthenticationHandler>();
            services.AddSingleton<UserClaimsHandler>();

            services.AddSwaggerGen(swagger =>
            { 
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Crafts.fo API",
                    Description = "ASP.NET 5 Core Web API"
                });

                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                    },
                    new string[] { }
                }
                });
            });

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer();

            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: MyAllowSpecificOrigins,
                    builder => builder
                        .WithOrigins("http://localhost:4200", "https://localhost:4200", "http://89.187.103.53", "http://instantcms.dk")
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                );
            });

            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
                hubOptions.ClientTimeoutInterval = new System.TimeSpan(0, 0, 10);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();
            app.UseAuthentication();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            // app.UseSwaggerAuthorized(Configuration["AllowSwaggerAccessFor"]);
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint("v1/swagger.json", "CraftsApi V1");
                swagger.DisplayRequestDuration();
                swagger.EnableDeepLinking();
                swagger.DisplayOperationId();
            });

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                    .RequireCors(MyAllowSpecificOrigins)
                    .RequireAuthorization();

                endpoints.MapHub<MessageHub>("/messagehub");
                endpoints.MapHub<PageHub>("/pagehub");
            });
        }
    }
}
