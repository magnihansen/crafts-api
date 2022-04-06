using System;
namespace CraftsApi.Auth
{
    /// <summary>
    /// Provides functionality for encoding a byte array as a url-encoded base64 string,
    /// thus making the base64 url-safe. 
    /// </summary>
    public interface IBase64UrlEncoder
    {
        /// <summary>
        /// Encode a byte array of data as a url-safe base64 encoding.
        /// </summary>
        /// <param name="data">Byte array of data to encode.</param>
        /// <returns>A url-safe base64 encoded string.</returns>
        string Encode(byte[] data);

        /// <summary>
        /// Decode the given url-safe base64 encoded string as a byte array of data.
        /// </summary>
        /// <param name="urlSafeBase64String">An url-safe base64 encoded string to decode.</param>
        /// <returns>The decoded byte array of data.</returns>
        byte[] Decode(string urlSafeBase64String);
    }
}
