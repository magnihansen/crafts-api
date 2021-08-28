using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CraftsApi.Service;
using CraftsApi.Service.Authentication;
using CraftsApi.Service.Requests;
using CraftsApi.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CraftsApi.Controllers.V1
{
    [Version(1)]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IJwtManager _jwtManager;

        public UserController(
            IUserService userService,
            IJwtManager jwtManager)
        {
            _userService = userService;
            _jwtManager = jwtManager;
        }

        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<User>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return new OkObjectResult(users.ToList());
        }

        [Authorize]
        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _userService.GetUserAsync(userId);
            return new OkObjectResult(user);
        }

        [Authorize]
        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserByIdentity()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return NotFound(identity);
            }
            User user = await _jwtManager.GetUserByIdentity(identity);
            if (user == null)
            {
                return NotFound();
            }
            return new OkObjectResult(user);
        }

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
        {
            bool userAdded = await _userService.AddUserAsync(addUserRequest);
            return new OkObjectResult(userAdded);
        }

        [HttpPut]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest updateUserRequest)
        {
            bool userUpdated = await _userService.UpdateUserAsync(updateUserRequest);
            return new OkObjectResult(userUpdated);
        }

        [HttpDelete]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            bool userDeleted = await _userService.DeleteUserAsync(userId);
            return new OkObjectResult(userDeleted);
        }
    }
}
 