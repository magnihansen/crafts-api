using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.Application;
using CraftsApi.Service.Mappings;
using CraftsApi.Service.Requests;

namespace CraftsApi.Service
{
    public class UserService : IUserService
    {
        private readonly IUserApplication _userApplication;

        public UserService(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        public async Task<List<ViewModels.User>> GetUsersAsync()
        {
            List<DomainModels.User> users = await _userApplication.GetUsersAsync();
            return users.MapListOfDomainUsersToListOfViewUsers();
        }

        public async Task<ViewModels.User> GetUserAsync(int userId)
        {
            DomainModels.User user = await _userApplication.GetUserAsync(userId);
            return user.MapDomainUserToViewUser();
        }

        public async Task<ViewModels.User> GetUserByCredientialsAsync(string username, string password)
        {
            DomainModels.User user = await _userApplication.GetUserByCredientialsAsync(username, password);
            return user.MapDomainUserToViewUser();
        }

        public async Task<bool> AddUserAsync(AddUserRequest addUserRequest)
        {
            var user = addUserRequest.MapAddUserRequestToUser();
            var added = await _userApplication.AddUserAsync(user);
            return added;
        }

        public async Task<bool> UpdateUserAsync(UpdateUserRequest updateUserRequest)
        {
            var user = updateUserRequest.MapUpdateUserRequestToUser();
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
