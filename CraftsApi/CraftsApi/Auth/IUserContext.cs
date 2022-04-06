using System;
using System.Security.Principal;

namespace CraftsApi.Auth
{
    public interface IUserContext : IPrincipal
    {
        IUserIdentity User { get; }

        string JwtToken { get; }

        DateTimeOffset? JwtTokenExpires { get; }
    }
}
