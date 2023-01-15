using System.Text;
using CraftsApi.Repository;
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
            services.AddAuthorization();
            services.AddSingleton<IJwtManager, JwtManager>();

            // for background service worker
            services.AddSingleton<IPageWorker, PageWorker>();

            services.AddSingleton<IDataAccess>(_ =>
                new DataAccess.DataAccess(
                    Configuration["ConnectionStrings:Default"].ToString(),
                    _webHostEnvironment
                )
            );

            services.AddSingleton<IImageGalleryService, ImageGalleryService>();
            services.AddSingleton<IImageGalleryTypeService, ImageGalleryTypeService>();
            services.AddSingleton<ICdnTokenService, CdnTokenService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<IPageTypeService, PageTypeService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ISettingService, SettingService>();
            services.AddSingleton<IDataService, DataService>();

            services.AddSingleton<IImageGalleryRepository, ImageGalleryRepository>();
            services.AddSingleton<IImageGalleryTypeRepository, ImageGalleryTypeRepository>();
            services.AddSingleton<ICdnTokenRepository, CdnTokenRepository>();
            services.AddSingleton<IPageRepository, PageRepository>();
            services.AddSingleton<IPageTypeRepository, PageTypeRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IDomainRepository, DomainRepository>();
            services.AddSingleton<ISettingRepository, SettingRepository>();
            services.AddSingleton<IDataRepository, DataRepository>();

            services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();

            services.AddSingleton<BasicAuthenticationHandler>(); // handles auth
            services.AddSingleton<UserClaimsHandler>();

            services.AddSwaggerGen(swagger =>
            { 
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Crafts.fo API",
                    Description = ".NET 6 Core Web API"
                });

                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please type 'Bearer' followed by the token into field",
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

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(Configuration["Jwt:SecurityKey"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:ValidIssuer"],
                    ValidAudience = Configuration["Jwt:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: MyAllowSpecificOrigins,
                    builder => builder
                        .WithOrigins(
                            "http://localhost:4200",
                            "https://localhost:4200",
                            "http://89.187.103.53",
                            "http://craftsfo.instantcms.dk",
                            "http://beautify-by-h.instantcms.dk"
                        )
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

            app.UseAuthentication();
            app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            // app.UseSwaggerAuthorized(Configuration["AllowSwaggerAccessFor"]);
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint("v1/swagger.json", "CraftsApi V1");
                swagger.DefaultModelsExpandDepth(-1);
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
