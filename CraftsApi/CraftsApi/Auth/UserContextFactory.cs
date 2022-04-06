using System;
using System.Security.Principal;
using CraftsApi.Helpers;
using Microsoft.AspNetCore.Http;

namespace CraftsApi.Auth
{
    public class UserContextFactory : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserClaimsHandler _claimsHandler;

        public UserContextFactory(
            IHttpContextAccessor httpContextAccessor,
            UserClaimsHandler claimsHandler)
        {
            _httpContextAccessor = httpContextAccessor;
            _claimsHandler = claimsHandler;
        }

        public IUserIdentity User => GetUserContext().User;

        public string JwtToken => GetUserContext().JwtToken;

        public DateTimeOffset? JwtTokenExpires => GetUserContext().JwtTokenExpires;

        public IIdentity Identity => GetUserContext().Identity;

        public bool IsInRole(string role)
        {
            return GetUserContext().IsInRole(role);
        }

        private const string UserContextStoreKey = "IUserContext";

        private IUserContext GetUserContext()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null && httpContext.Items.ContainsKey(UserContextStoreKey) && httpContext.Items[UserContextStoreKey] is IUserContext)
            {
                return (IUserContext)httpContext.Items[UserContextStoreKey];
            }

            IUserContext userContext = null;
            var user = httpContext?.User;

            if (user != null)
            {
                var userClaims = _claimsHandler.GetUserClaims(user.Claims, out var authToken, out var schemeName);

                var userIdentity = new UserIdentity(userClaims.UserId, userClaims.Username, userClaims.Fullname, userClaims.Firstname, userClaims.Lastname, schemeName, true);
                userContext = new UserContext(userIdentity, authToken, userClaims.ExpiresAt.FromEpochAsDateTimeOffset());

                httpContext.Items.Add(UserContextStoreKey, userContext);
            }
            else
            {
                userContext = new UserContext(new UserIdentity(0, string.Empty, null, false), null, null);
            }

            return userContext;
        }
    }
}
