using System;
using System.Linq;
using System.Text;
using CraftsApi.Application;
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
using Microsoft.IdentityModel.Tokens;
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

            services.AddSwaggerGen(swagger =>
            {
                swagger.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                //This is to generate the Default UI of Swagger Documentation  
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Crafts.fo API",
                    Description = "ASP.NET Core 5.0 Web API"
                });
                // To Enable authorization using Swagger (JWT)  
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:ValidIssuer"].ToString(),
                    ValidAudience = Configuration["Jwt:ValidAudience"].ToString(),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecurityKey"].ToString())),
                    ClockSkew = TimeSpan.Zero
                };
            });

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
                
            }

            app.UseDeveloperExceptionPage();

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
