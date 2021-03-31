using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.Service.Requests;

namespace CraftsApi.Service
{
    public interface IUserService
    {
        Task<List<ViewModels.User>> GetUsersAsync();

        Task<ViewModels.User> GetUserAsync(int userId);

        Task<ViewModels.User> GetUserByCredientialsAsync(string username, string password);

        Task<bool> AddUserAsync(AddUserRequest addUserRequest);

        Task<bool> UpdateUserAsync(UpdateUserRequest updateUserRequest);

        Task<bool> DeleteUserAsync(int userId);
    }
}
