using System;
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<IActionResult> Login(AuthenticateRequest authenticateRequest)
        {
            string token = await _jwtManager.Authenticate(authenticateRequest.Username, authenticateRequest.Password);
            if (token == null)
            {
                return Unauthorized("Invalid login");
            }
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<IActionResult> ValidateToken(string token)
        {
            Tuple<bool, string> tokenValidation = await Task.FromResult(_jwtManager.ValidateCurrentToken(token));
            if (tokenValidation.Item1)
            {
                return Ok(true);
            }
            else
            {
                return Unauthorized(tokenValidation.Item2);
            }
        }
    }
}
 