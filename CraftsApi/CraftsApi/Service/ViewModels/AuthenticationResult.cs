namespace CraftsApi.Service.ViewModels
{
    /// <summary>
    /// Result of a successful authentication.
    /// </summary>
    public class AuthenticationResult
    {
        public string Token { get; }
        public long Expires { get; }
        public string Message { get; set; }

        public AuthenticationResult(string token, long expires, string message)
        {
            Token = token;
            Expires = expires;
            Message = message;
        }
    }
}
