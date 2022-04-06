using System;
namespace CraftsApi.Auth
{
    /// <summary>
    /// Default values used by AuthToken authentication.
    /// </summary>
    public static class AuthTokenDefaults
    {
        /// <summary>
        /// Default value for AuthenticationScheme property in the AuthTokenAuthenticationOptions
        /// </summary>
        public const string AuthenticationScheme = "JWT";
    }
}
