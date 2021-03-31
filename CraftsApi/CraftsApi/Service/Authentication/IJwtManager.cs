using System.Threading.Tasks;

namespace CraftsApi.Service.Authentication
{
    public interface IJwtManager
    {
        Task<string> Authenticate(string username, string password);
    }
}
