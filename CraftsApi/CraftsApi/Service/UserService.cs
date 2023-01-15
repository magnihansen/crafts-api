using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.Repository;
using CraftsApi.Service.Mappings;
using CraftsApi.Controllers.V1.Requests;

namespace CraftsApi.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userApplication;

        public UserService(IUserRepository userApplication)
        {
            _userApplication = userApplication;
        }

        public async Task<List<ViewModels.UserVM>> GetUsersAsync()
        {
            List<DomainModels.User> users = await _userApplication.GetUsersAsync();
            return users.MapUserToUserVM();
        }

        public async Task<ViewModels.UserVM> GetUserAsync(int userId)
        {
            DomainModels.User user = await _userApplication.GetUserAsync(userId);
            return user.MapUserToUserVM();
        }

        public async Task<ViewModels.UserVM> GetUserByCredientialsAsync(string username, string password)
        {
            DomainModels.User user = await _userApplication.GetUserByCredientialsAsync(username, password);
            if (user == null)
            {
                return null;
            }
            return user.MapUserToUserVM();
        }

        public async Task<bool> AddUserAsync(DomainModels.User user)
        {
            var added = await _userApplication.InsertUserAsync(user);
            return added;
        }

        public async Task<bool> UpdateUserAsync(DomainModels.User user)
        {
            var updated = await _userApplication.UpdateUserAsync(user);
            return updated;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            bool deleted = await _userApplication.DeleteUserAsync(userId);
            return deleted;
        }
    }
}
