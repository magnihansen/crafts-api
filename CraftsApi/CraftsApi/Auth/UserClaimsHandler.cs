using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using CraftsApi.Helpers;

namespace CraftsApi.Auth
{
    public class UserClaimsHandler
    {
        private const string UserClaims = "UserClaims";
        private const string AuthToken = "AuthToken";

        public List<Claim> GetClaims(UserClaims userClaims, string authToken, string schemeName)
        {
            var userClaimsJson = JsonSerializer.Serialize(userClaims);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.AuthenticationMethod, schemeName),
                new Claim(AuthToken, authToken),
                new Claim(UserClaims, userClaimsJson)
            };

            return claims;
        }

        public UserClaims GetUserClaims(IEnumerable<Claim> claims, out string authToken, out string schemeName)
        {
            var userClaimsJson = claims.FirstOrDefault(t => t.Type == UserClaims)?.Value;
            var userClaims = JsonSerializer.Deserialize<UserClaims>(userClaimsJson);

            schemeName = claims.FirstOrDefault(t => t.Type == ClaimTypes.AuthenticationMethod)?.Value;
            authToken = claims.FirstOrDefault(t => t.Type == AuthToken)?.Value;

            return userClaims;
        }

        public IUserContext CreateUserContext(UserClaims userClaims, string jwtToken, string schemeName)
        {
            var isAuthenticated = userClaims.UserId > 0;
            var userIdentity = new UserIdentity(userClaims.UserId, userClaims.Username, schemeName, isAuthenticated);

            return new UserContext(userIdentity, jwtToken, userClaims.ExpiresAt.FromEpochAsDateTimeOffset());
        }

    }
}
