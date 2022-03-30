using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CraftsApi.Service.Authentication
{
    public interface IJwtManager
    {
        Task<string> Authenticate(string username, string password);

        Task<ViewModels.User> GetUserByIdentity(ClaimsIdentity identity);

        Tuple<bool, string> ValidateCurrentToken(string token);
    }
}
