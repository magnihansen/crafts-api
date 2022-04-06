using System;
namespace CraftsApi.Auth
{
    public class Base64UrlEncoder : IBase64UrlEncoder
    {
        /// <inheritdoc />
        public string Encode(byte[] data)
        {
            Check.NotNull(data, nameof(data));

            if (data.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(data));

            var result = Convert.ToBase64String(data);
            result = result.TrimEnd('=');
            result = result.Replace('+', '-');
            result = result.Replace('/', '_');
            return result;
        }

        /// <inheritdoc />
        public byte[] Decode(string urlSafeBase64String)
        {
            Check.NotEmpty(urlSafeBase64String, nameof(urlSafeBase64String));

            var result = urlSafeBase64String;
            result = result.Replace('_', '/');
            result = result.Replace('-', '+');
            result = AppendBase64Padding(result);

            return Convert.FromBase64String(result);
        }

        private string AppendBase64Padding(string str)
        {
            switch (GetBase64PaddingMode(str))
            {
                case Base64Padding.NoPadding:
                    break;
                case Base64Padding.OnePaddingCharacter:
                    str += "=";
                    break;
                case Base64Padding.TwoPaddingCharacters:
                    str += "==";
                    break;
                default:
                    throw new FormatException("Illegal url-safe base64 encoded string.");
            }
            return str;
        }

        private Base64Padding GetBase64PaddingMode(string str) => (Base64Padding)(str.Length % 4);

        private enum Base64Padding
        {
            NoPadding = 0,
            TwoPaddingCharacters = 2,
            OnePaddingCharacter = 3
        }
    }
}
