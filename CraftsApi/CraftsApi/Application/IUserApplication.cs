using System.Collections.Generic;
using System.Threading.Tasks;

namespace CraftsApi.Application
{
    public interface IUserApplication
    {
        Task<List<DomainModels.User>> GetUsersAsync();

        Task<DomainModels.User> GetUserAsync(int userId);

        Task<DomainModels.User> GetUserByCredientialsAsync(string username, string password);

        Task<bool> AddUserAsync(DomainModels.User user);

        Task<bool> UpdateUserAsync(DomainModels.User user);

        Task<bool> DeleteUserAsync(int userId);
    }
}
