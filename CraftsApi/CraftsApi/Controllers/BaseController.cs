using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace CraftsApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("V[version]/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController : ControllerBase
    {
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class VersionAttribute : RouteValueAttribute
    {
        public VersionAttribute(int version) : base("version", version.ToString())
        {
        }
    }
}
