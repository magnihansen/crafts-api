using System;
namespace CraftsApi.Auth
{
    /// <summary>
    /// Provides decoding of a Json Web Token to a collection of claims.
    /// </summary>
    /// <typeparam name="TClaimsCollection">Type of token claims collection to decode to.</typeparam>
    public interface IJwtDecoder<TClaimsCollection>
        where TClaimsCollection : class, new()
    {
        /// <summary>
        /// Decode the given Json Web Token into a collection of values.
        /// </summary>
        /// <param name="jsonWebToken">Json Web Token to decode.</param>
        TClaimsCollection Decode(string jsonWebToken);
    }
}
