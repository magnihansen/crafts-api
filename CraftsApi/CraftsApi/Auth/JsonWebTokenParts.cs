using System;
using System.Collections.Generic;
using System.Linq;

namespace CraftsApi.Auth
{
    /// <summary>
    /// Represents the different parts of a Json Web Token.
    /// </summary>
    internal class JsonWebTokenParts
    {
        public const char TokenSeparator = '.';

        private readonly string[] _parts;

        /// <summary>
        /// Gets whether this token is a headless Json Web Token.
        /// </summary>
        public bool IsHeadless { get; }

        /// <summary>
        /// Gets the Json Web Token parts.
        /// </summary>
        public IReadOnlyCollection<string> Parts { get { return _parts; } }

        /// <summary>
        /// Gets the header of the Json Web Token.
        /// </summary>
        public string Header { get { return _parts[0]; } }

        /// <summary>
        /// Gets the header of the Json Web Token.
        /// </summary>
        public string Payload { get { return _parts[1]; } }

        /// <summary>
        /// Gets the header of the Json Web Token.
        /// </summary>
        public string Signature { get { return _parts[2]; } }

        private JsonWebTokenParts(string[] parts, bool isHeadless)
        {
            _parts = parts;
            IsHeadless = isHeadless;
        }

        /// <summary>
        /// Separate a Json Web Token string into it's respective parts.
        /// </summary>
        /// <param name="token">Json Web Token to get separate parts for.</param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentException"></exception>
	    public static JsonWebTokenParts FromString(string token)
        {
            var parts = SplitToken(token);
            if (parts.Length != 3)
            {
                throw new FormatException("Given token has illegal number of token parts.");
            }

            return new JsonWebTokenParts(parts, isHeadless: false);
        }

        /// <summary>
        /// Separate a headless Json Web Token string into it's respective parts.
        /// </summary>
        /// <param name="token">Json Web Token to get separate parts for.</param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static JsonWebTokenParts FromHeadlessString(string token)
        {
            var parts = SplitToken(token);
            if (parts.Length != 2)
            {
                throw new FormatException("Given token has illegal number of token parts.");
            }

            var emptyHeader = new string[] { null };

            return new JsonWebTokenParts(emptyHeader.Concat(parts).ToArray(), isHeadless: true);
        }

        private static string[] SplitToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentException(nameof(token));

            return token.Split(new[] { TokenSeparator }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
