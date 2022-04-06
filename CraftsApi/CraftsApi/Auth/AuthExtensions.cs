using System;
using Microsoft.AspNetCore.Authentication;

namespace CraftsApi.Auth
{
    public static class AuthExtensions
    {
        public static AuthenticationBuilder AddAuthTokenAuthentication(
            this AuthenticationBuilder builder,
            Action<AuthTokenOptions> configureOptions
        )
        {
            return builder.AddScheme<AuthTokenOptions, AuthTokenAuthenticationHandler>(
                AuthTokenDefaults.AuthenticationScheme,
                configureOptions
            );
        }
    }
}
