using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NetTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CraftsApi.Swagger
{
    public class SwaggerAuthorizedMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SwaggerAuthorizedMiddleware> _logger;
        private readonly string[] _safelist;

        public SwaggerAuthorizedMiddleware(
            RequestDelegate next,
            ILogger<SwaggerAuthorizedMiddleware> logger,
            string safelist)
        {
            _next = next;
            _logger = logger;
            _safelist = safelist.Split(';');
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                var remoteIp = context.Connection.RemoteIpAddress;
                _logger.LogDebug("Request from Remote IP address: {RemoteIp}", remoteIp);

                var remoteIpBytes = remoteIp.GetAddressBytes();
                var badIp = true;
                foreach (var address in _safelist)
                {
                    if (IsIpSafe(address, remoteIp, remoteIpBytes))
                    {
                        badIp = false;
                        break;
                    }
                }

                if (badIp)
                {
                    _logger.LogWarning(
                        "Forbidden Request from Remote IP address: {RemoteIp}", remoteIp);
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return;
                }
            }

            await _next.Invoke(context);
        }

        private bool IsIpSafe(string address, IPAddress remoteIp, byte[] remoteIpBytes)
        {
            if (address.IndexOf('/') > -1)
            {
                var whitelistIpRange = IPAddressRange.Parse(address);
                return whitelistIpRange.Contains(remoteIp);
            }
            else
            {
                var testIp = IPAddress.Parse(address);
                return testIp.GetAddressBytes().SequenceEqual(remoteIpBytes);
            }
        }
    }

    public static class SwaggerAuthorizeExtensions
    {
        public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder, string safelist)
        {
            return builder.UseMiddleware<SwaggerAuthorizedMiddleware>(safelist);
        }

        public static void AddJwtSecurityRequirement(this Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions swagger)
        {
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = $"Authorization header using the 'Bearer' scheme.\n\rEnter 'Bearer' [space] and then your token in the text input below.\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "bearer",
                BearerFormat = "Bearer"
            });

            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        },
                    },
                    new List<string>()
                }
            });
        }
    }
}
