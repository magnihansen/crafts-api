using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CraftsApi.Controllers.V1
{
    [Version(1)]
    public class LogController : BaseController
    {
        [AllowAnonymous]
        [HttpPut]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SaveError(string message)
        {
            if (message == null)
            {
                return NotFound("No message sent");
            }

            await Task.Run(() => null);

            return Ok();
        }
    }
}
