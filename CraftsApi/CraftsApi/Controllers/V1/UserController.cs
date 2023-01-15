using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CraftsApi.Auth;
using CraftsApi.Service;
using CraftsApi.Service.Authentication;
using CraftsApi.Controllers.V1.Requests;
using CraftsApi.Service.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CraftsApi.Service.Mappings;

namespace CraftsApi.Controllers.V1
{
    [Version(1)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IJwtManager _jwtManager;
        private readonly IJWTManagerRepository _jWTManagerRepository;

        public UserController(
            IUserService userService,
            IJwtManager jwtManager,
            IJWTManagerRepository jWTManagerRepository)
        {
            _userService = userService;
            _jwtManager = jwtManager;
            _jWTManagerRepository = jWTManagerRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserVM>))]
        public async Task<IActionResult> GetUsers()
        {
            List<UserVM> users = await _userService.GetUsersAsync();

            return Ok(users.ToList());
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _userService.GetUserAsync(userId);
            if (user is null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserByIdentity()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return NotFound(identity);
            }
            UserVM user = await _jwtManager.GetUserByIdentity(identity);
            if (user == null)
            {
                return NotFound("User identity not found");
            }
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
        {
            var user = addUserRequest.MapAddUserRequestToUser();
            bool userAdded = await _userService.AddUserAsync(user);
            return Ok(userAdded);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest updateUserRequest)
        {
            var user = updateUserRequest.MapUpdateUserRequestToUser();
            bool userUpdated = await _userService.UpdateUserAsync(user);
            return Ok(userUpdated);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            bool userDeleted = await _userService.DeleteUserAsync(userId);
            return Ok(userDeleted);
        }

        [HttpGet]
        public List<string> Get()
        {
            var users = new List<string>
            {
                "Satinder Singh",
                "Amit Sarna",
                "Davin Jon"
            };

            return users;
        }

        /// <summary>
        /// Can this method be removed ?
        /// </summary>
        /// <param name="usersdata"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate(Users usersdata)
        {
            var token = _jWTManagerRepository.Authenticate(usersdata);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
 