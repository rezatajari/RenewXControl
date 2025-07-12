using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace RenewXControl.Api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected string? UserId => User?.FindFirst(type:ClaimTypes.NameIdentifier)?.Value;
    }
}
