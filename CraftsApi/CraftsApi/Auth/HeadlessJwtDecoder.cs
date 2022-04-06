using System;
using System.Text;
using Newtonsoft.Json;

namespace CraftsApi.Auth
{
    /// <summary>
    /// A Json Web Token decoder for decoding and validating the payload of a headless Json Web Token.
    /// 
    /// <para>
    ///     A headless-JWT has a smaller footprint and does not convey any information 
    ///     about which algorithm is used to sign the token. 
    ///     Thus the signing algorithm is implicitly defined by the context the token exists in,
    ///     making the JWT slightly more secure.
    /// </para>
    /// </summary>
    /// <typeparam name="TClaimsCollection">Type of token claims collection to decode to.</typeparam>
    public class HeadlessJwtDecoder<TClaimsCollection> : IJwtDecoder<TClaimsCollection>
            where TClaimsCollection : class, new()
    {
        private readonly IBase64UrlEncoder _base64UrlEncoder;

        /// <summary>
        /// Initializes the Json Web Token decoder.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public HeadlessJwtDecoder(IBase64UrlEncoder base64UrlEncoder)
        {
            _base64UrlEncoder = base64UrlEncoder ?? throw new ArgumentNullException(nameof(base64UrlEncoder));
        }

        /// <summary>
        /// Decodes the given headless Json Web Token and returns the decoded payload claims.
        /// </summary>
        /// <param name="jsonWebToken">Token to decode.</param>
        /// <param name="validate">Whether to check the validity of the token. (Default is true)</param>
        /// <returns>Decoded token payload.</returns
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public virtual TClaimsCollection Decode(string jsonWebToken)
        {
            Check.NotEmpty(jsonWebToken, nameof(jsonWebToken));

            var parts = JsonWebTokenParts.FromHeadlessString(jsonWebToken);
            var decodedClaims = DeserializeTokenPayload(parts.Payload);

            return decodedClaims;
        }

        private TClaimsCollection DeserializeTokenPayload(string payload)
        {
            var payloadBytes = _base64UrlEncoder.Decode(payload);
            var payloadJson = GetString(payloadBytes);
            return JsonConvert.DeserializeObject<TClaimsCollection>(payloadJson);
        }

        private string GetString(byte[] data) => Encoding.UTF8.GetString(data);
    }
}
