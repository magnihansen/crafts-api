using System;
using System.Security.Principal;

namespace CraftsApi.Auth
{
    public class UserContext : IUserContext
    {
        public UserContext(
            IUserIdentity user,
            string jwtToken,
            DateTimeOffset? jwtTokenExpires)
        {
            User = user;
            JwtToken = jwtToken;
            JwtTokenExpires = jwtTokenExpires;
        }

        public IUserIdentity User { get; }

        public string JwtToken { get; }

        public DateTimeOffset? JwtTokenExpires { get; }

        public IIdentity Identity => User;

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}
