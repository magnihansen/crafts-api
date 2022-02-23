using System.Threading.Tasks;
using CraftsApi.Service.Authentication;
using CraftsApi.Service.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CraftsApi.Controllers.V1
{
    [Version(1)]
    public class AuthController : BaseController
    {
        private readonly IJwtManager _jwtManager;

        public AuthController(IJwtManager jwtManager)
        {
            _jwtManager = jwtManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> Login(AuthenticateRequest authenticateRequest)
        {
            string token = await _jwtManager.Authenticate(authenticateRequest.Username, authenticateRequest.Password);
            if (token == null)
            {
                return BadRequest("Invalid login");
            }
            return Ok(token);
        }
    }
}
 