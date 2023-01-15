using Microsoft.AspNetCore.Authentication;

namespace CraftsApi.Auth
{
    public class BasicAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string Realm { get; set; }
    }
}
