using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.Controllers.V1.Requests;

namespace CraftsApi.Service
{
    public interface IUserService
    {
        Task<List<ViewModels.UserVM>> GetUsersAsync();

        Task<ViewModels.UserVM> GetUserAsync(int userId);

        Task<ViewModels.UserVM> GetUserByCredientialsAsync(string username, string password);

        Task<bool> AddUserAsync(DomainModels.User user);

        Task<bool> UpdateUserAsync(DomainModels.User user);

        Task<bool> DeleteUserAsync(int userId);
    }
}
