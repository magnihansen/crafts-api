using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CraftsApi.Service;
using CraftsApi.Service.Requests;
using CraftsApi.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CraftsApi.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(
            IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<User>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return new OkObjectResult(users.ToList());
        }

        [HttpGet("[action]/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _userService.GetUserAsync(userId);
            return new OkObjectResult(user);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
        {
            bool userAdded = await _userService.AddUserAsync(addUserRequest);
            return new OkObjectResult(userAdded);
        }

        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest updateUserRequest)
        {
            bool userUpdated = await _userService.UpdateUserAsync(updateUserRequest);
            return new OkObjectResult(userUpdated);
        }

        [HttpDelete("[action]/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            bool userDeleted = await _userService.DeleteUserAsync(userId);
            return new OkObjectResult(userDeleted);
        }
    }
}
 