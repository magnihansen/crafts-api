using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace CraftsApi.Auth
{
    public sealed class ApiRequest
    {
        #region Shared

        private static readonly Lazy<HttpClient> _lazyHttpClient = new Lazy<HttpClient>(() => new HttpClient() { Timeout = TimeSpan.FromHours(1) });

        public static HttpClient HttpClient => _lazyHttpClient.Value;

        public static class MediaType
        {
            public const string Application_X_www_form_urlencoded = "application/x-www-form-urlencoded";
            public const string Application_Json = "application/json";
            public const string Text_Plain = "text/plain";
            public const string Text_Html = "text/html";
        }

        // See the full list and the options for each method here: https://developer.mozilla.org/en-US/docs/Web/HTTP/Methods
        public enum HttpMethod
        {
            Get,
            Head,
            Post,
            Put,
            Delete,
            Trace,
            Options
        }

        private static string Serialize(object value)
        {
            return
                value != null
                ? JsonConvert.SerializeObject(value/*, ApiJsonFormatter.Instance.SerializerSettings*/)
                : null;
        }

        private static T Deserialize<T>(string value)
        {
            return
                !string.IsNullOrWhiteSpace(value)
                ? JsonConvert.DeserializeObject<T>(value/*, ApiJsonFormatter.Instance.SerializerSettings*/)
                : default;
        }

        public class JsonResponse
        {
            public bool Success { get; set; }

            public string Message { get; set; }
        }

        public class JsonResponse<T> : JsonResponse
        {
            public T Data { get; set; }
        }

        private static async Task<ApiResponse> InvokeHttpClientAsync(Func<Task<HttpResponseMessage>> clientAction)
        {
            var result = await clientAction.Invoke().ConfigureAwait(false);
            var response = await result?.Content?.ReadAsStringAsync();
            var statusCode = result?.StatusCode ?? 0;

            switch (statusCode)
            {
                case HttpStatusCode.OK:
                    return new ApiResponse
                    {
                        Content = response,
                        Headers = result?.Headers
                    };

                case HttpStatusCode.NoContent:
                case HttpStatusCode.ResetContent:
                    return null;

                default:
                    {
                        string errorMessage;

                        try
                        {
                            errorMessage = Deserialize<JsonResponse>(response).Message;
                            if (string.IsNullOrWhiteSpace(errorMessage))
                            {
                                errorMessage = response;
                            }
                        }
                        catch
                        {
                            errorMessage = response;
                        }

                        throw new Exception(errorMessage);
                    }
            }
        }

        #endregion Shared

        #region Setup

        private readonly IDictionary<string, string> _headers = new Dictionary<string, string>();
        private readonly List<MediaTypeWithQualityHeaderValue> _accepts = new List<MediaTypeWithQualityHeaderValue>();
        private AuthenticationHeaderValue _authentication;
        private Action<HttpRequestMessage> _beforeSendAction;
        private readonly QueryStringHelper _queryStringHelper;

        private ApiRequest(string url)
        {
            _queryStringHelper = new QueryStringHelper(url);
        }

        public static ApiRequest Create(string url)
        {
            return new ApiRequest(url);
        }

        public ApiRequest AddParameter(string key, object value)
        {
            _queryStringHelper.AddParameter(key, value);
            return this;
        }

        public ApiRequest Accept(string mediaType)
        {
            _accepts.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            return this;
        }

        public ApiRequest BeforeSend(Action<HttpRequestMessage> messageAction)
        {
            _beforeSendAction = messageAction;
            return this;
        }

        public ApiRequest Authentication(string scheme, string parameter)
        {
            _authentication = new AuthenticationHeaderValue(scheme, parameter);
            return this;
        }

        public ApiRequest AddHeader(string name, string value)
        {
            _headers.Add(name, value);
            return this;
        }

        public ApiRequest AddHeaders(IDictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                _headers.Add(header);
            }

            return this;
        }

        #endregion Setup

        #region Methods

        public async Task<ApiResponse> DeleteAsync(object jsonObject = null)
        {
            return await ExecuteRequestAsync(HttpMethod.Delete, jsonObject).ConfigureAwait(false);
        }

        public async Task<TResponse> DeleteAsync<TResponse>(object jsonObject = null)
        {
            return await ExecuteRequestAsync<TResponse>(HttpMethod.Delete, jsonObject).ConfigureAwait(false);
        }

        public async Task<ApiResponse> GetAsync()
        {
            return await ExecuteRequestAsync(HttpMethod.Get).ConfigureAwait(false);
        }

        public async Task<TResponse> GetAsync<TResponse>()
        {
            return await ExecuteRequestAsync<TResponse>(HttpMethod.Get).ConfigureAwait(false);
        }

        public async Task<HttpResponseHeaders> HeadAsync()
        {
            return (await ExecuteRequestAsync(HttpMethod.Head).ConfigureAwait(false))?.Headers;
        }

        public async Task<ApiResponse> OptionsAsync()
        {
            return await ExecuteRequestAsync(HttpMethod.Options).ConfigureAwait(false);
        }

        public async Task<TResponse> OptionsAsync<TResponse>()
        {
            return await ExecuteRequestAsync<TResponse>(HttpMethod.Options).ConfigureAwait(false);
        }

        public async Task<ApiResponse> PostAsync(object jsonObject = null)
        {
            return await ExecuteRequestAsync(HttpMethod.Post, jsonObject).ConfigureAwait(false);
        }

        public async Task<TResponse> PostAsync<TResponse>(object jsonObject = null)
        {
            return await ExecuteRequestAsync<TResponse>(HttpMethod.Post, jsonObject).ConfigureAwait(false);
        }

        public async Task<ApiResponse> PutAsync(object jsonObject = null)
        {
            return await ExecuteRequestAsync(HttpMethod.Put, jsonObject).ConfigureAwait(false);
        }

        public async Task<TResponse> PutAsync<TResponse>(object jsonObject = null)
        {
            return await ExecuteRequestAsync<TResponse>(HttpMethod.Put, jsonObject).ConfigureAwait(false);
        }

        public async Task<ApiResponse> ExecuteRequestAsync(HttpMethod method, object jsonObject = null, bool serializeObject = true)
        {
            var content = serializeObject ? Serialize(jsonObject) : jsonObject as string;
            return await InvokeHttpClientAsync(() => HttpClient.SendAsync(GetRequestMessage(method, content))).ConfigureAwait(false);
        }

        public async Task<TResponse> ExecuteRequestAsync<TResponse>(HttpMethod method, object jsonObject = null, bool serializeObject = true)
        {
            var json = (await ExecuteRequestAsync(method, jsonObject, serializeObject).ConfigureAwait(false))?.Content;
            return Deserialize<TResponse>(json);
        }

        #endregion Methods

        #region Helpers

        private HttpRequestMessage GetRequestMessage(HttpMethod method, string content = null)
        {
            var requestUrl = _queryStringHelper.GetPathAndQuery();
            var requestMessage = new HttpRequestMessage(GetBclHttpMethod(method), requestUrl);

            if (!string.IsNullOrEmpty(content))
            {
                requestMessage.Content = new StringContent(content, Encoding.UTF8, MediaType.Application_Json);
            }

            if (!_accepts.IsNullOrEmpty())
            {
                foreach (var item in _accepts)
                {
                    requestMessage.Headers.Accept.Add(item);
                }
            }

            if (_authentication != null)
            {
                requestMessage.Headers.Authorization = _authentication;
            }

            foreach (var header in _headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }

            _beforeSendAction?.Invoke(requestMessage);

            return requestMessage;
        }

        private static System.Net.Http.HttpMethod GetBclHttpMethod(HttpMethod method)
        {
            switch (method)
            {
                case HttpMethod.Get:
                    return System.Net.Http.HttpMethod.Get;

                case HttpMethod.Head:
                    return System.Net.Http.HttpMethod.Head;

                case HttpMethod.Post:
                    return System.Net.Http.HttpMethod.Post;

                case HttpMethod.Put:
                    return System.Net.Http.HttpMethod.Put;

                case HttpMethod.Delete:
                    return System.Net.Http.HttpMethod.Delete;

                case HttpMethod.Trace:
                    return System.Net.Http.HttpMethod.Trace;

                case HttpMethod.Options:
                    return System.Net.Http.HttpMethod.Options;

                default:
                    throw new ArgumentException("Unknown HttpMethod: " + method, nameof(method));
            }
        }

        #endregion Helpers
    }
}
