using System.Threading.Tasks;
using CraftsApi.Auth;

namespace CraftsApi.Service
{
    public interface ICdnTokenService
    {
        Task<string> GenerateCdnTokenAsync(string host);
    }
}

