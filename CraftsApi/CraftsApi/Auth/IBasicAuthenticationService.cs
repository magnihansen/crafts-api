using System.Threading.Tasks;

namespace CraftsApi.Auth
{
    public interface IBasicAuthenticationService
    {
        Task<bool> IsValidUserAsync(string user, string password);
    }
}
