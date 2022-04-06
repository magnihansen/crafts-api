using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CraftsApi.Auth
{
    public class QueryStringHelper
    {
        private readonly List<KeyValuePair<string, string>> _parameters = new List<KeyValuePair<string, string>>();
        private readonly string _path;

        public QueryStringHelper()
        {
        }

        public QueryStringHelper(string path)
        {
            _path = path;
        }

        public QueryStringHelper AddParameter(string key, object value)
        {
            if (value == null)
            {
                return this;
            }

            var valueType = value.GetType();

            if (valueType.IsSimpleType())
            {
                if (value != null)
                {
                    _parameters.Add(new KeyValuePair<string, string>(key, GetParameterValue(value)));
                }
            }
            else if (valueType.IsEnumerable())
            {
                var i = 0;
                var enumerable = (IEnumerable)value;
                var elementTypeIsSimple = enumerable.GetElementTypeOfEnumerable().IsSimpleType();

                foreach (var element in enumerable)
                {
                    var indexKey = elementTypeIsSimple ? key : $"{key}[{i++}]";
                    AddParameter(indexKey, element);
                }
            }
            else if (valueType.IsClass || valueType.IsNonFormatableStruct())
            {
                foreach (var property in value.GetProperties())
                {
                    var propertyKey = $"{key}.{property.Name}";
                    var propertyValue = property.GetValue(value);

                    AddParameter(propertyKey, propertyValue);
                }
            }

            return this;
        }

        public string GetPathAndQuery(bool addQuestionmarkSeparator = true)
        {
            var url = new StringBuilder(_path);

            if (_parameters.Count > 0)
            {
                var pathHasQuestionMarkSeparator = _path?.Contains("?") ?? false;
                var pathHasExistingQueryStringValue = _path?.Contains("=") ?? false;

                if (addQuestionmarkSeparator && !pathHasQuestionMarkSeparator)
                {
                    url.Append("?");
                }

                if (pathHasQuestionMarkSeparator && pathHasExistingQueryStringValue)
                {
                    // We assume that the path already has some sort of querystring attached, so we append to this
                    url.Append("&");
                }

                foreach (var parameter in _parameters)
                {
                    url.AppendFormat("{0}={1}&", parameter.Key, System.Net.WebUtility.UrlEncode(parameter.Value));
                }

                url.Length = url.Length - 1;
            }

            return url.ToString();
        }

        public override string ToString()
        {
            return GetPathAndQuery();
        }

        private static string GetParameterValue(object value)
        {
            switch (value)
            {
                case null:
                    return null;

                case DateTime dateTimeValue:
                    return dateTimeValue.ToString("O");

                case DateTimeOffset dateTimeOffsetValue:
                    return dateTimeOffsetValue.ToString("O");

                case decimal decimalValue:
                    return decimalValue.ToString(System.Globalization.CultureInfo.InvariantCulture);

                case double doubleValue:
                    return doubleValue.ToString(System.Globalization.CultureInfo.InvariantCulture);

                case float floatValue:
                    return floatValue.ToString(System.Globalization.CultureInfo.InvariantCulture);

                default:
                    return value.ToString();
            }
        }
    }
}
