using System;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace CraftsApi.Helpers
{
    public static class StringHelper
    {
        public static string GetAddressHost(this Microsoft.AspNetCore.Http.HttpRequest httpRequest)
        {
            Uri address = httpRequest.GetTypedHeaders().Referer;
            if (address?.Host == "localhost")
            {
                return "craftsfo.instantcms.dk";
            }
            return address?.Host;
        }

        public static string ConvertToMySqlDateTime(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static object GetPropValue(this object src, string propName)
        {
            try
            {
                return src.GetType().GetProperty(propName).GetValue(src, null);
            }
            catch
            {
                return null;
            }
        }
    }
}
