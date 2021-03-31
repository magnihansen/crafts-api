using System.Threading.Tasks;
using CraftsApi.Service.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CraftsApi.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IJwtManager _jwtManager;

        public LoginController(IJwtManager jwtManager)
        {
            _jwtManager = jwtManager;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Authenticate(string username, string password)
        {
            string token = await _jwtManager.Authenticate(username, password);
            if (token == null)
            {
                return Unauthorized("Could not create token");
            }

            return Ok(token);
        }
    }
}
 