using System;
using System.Net.Http.Headers;

namespace CraftsApi.Auth
{
    public sealed class ApiResponse
    {
        // TODO: Change type to avoid System.Net.Http requirement
        public HttpResponseHeaders Headers { get; set; }

        public string Content { get; set; }
    }
}
