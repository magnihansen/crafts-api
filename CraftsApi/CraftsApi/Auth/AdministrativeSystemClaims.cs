using System;
namespace CraftsApi.Auth
{
    /// <summary>
    /// A catalog of authentication token claim keys, from the administrative system.
    /// </summary>
    public class AdministrativeSystemClaims
    {
        /// <summary>
        /// A claim representing the Unik Bolig access token.
        /// </summary>
        public string UnikAccessToken { get; set; }
    }
}
