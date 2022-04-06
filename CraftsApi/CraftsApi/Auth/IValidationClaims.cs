namespace CraftsApi.Auth
{
    /// <summary>
    /// Describes claims required by <see cref="JwtValidator"/>.
    /// </summary>
    public interface IValidationClaims
    {
        /// <summary>
        /// Gets or sets the Unix epoch timestamp at which a token becomes valid. 
        /// </summary>
        long NotBefore { get; set; }

        /// <summary>
        /// Gets or sets the Unix epoch timestamp at which a token expires.
        /// </summary>
        long ExpiresAt { get; set; }
    }
}
