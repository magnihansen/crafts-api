using System.Collections.Generic;
using System.Threading.Tasks;

namespace CraftsApi.Repository
{
    public interface IUserRepository
    {
        Task<List<DomainModels.User>> GetUsersAsync();

        Task<DomainModels.User> GetUserAsync(int userId);

        Task<DomainModels.User> GetUserByCredientialsAsync(string username, string password);

        Task<bool> InsertUserAsync(DomainModels.User user);

        Task<bool> UpdateUserAsync(DomainModels.User user);

        Task<bool> DeleteUserAsync(int userId);
    }
}
