using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected Guid UserId => TryGetUserId(out var id)
        ? id
        : throw new UnauthorizedAccessException("User ID is invalid.");

    private bool TryGetUserId(out Guid userId)
    {
        var idString = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(idString, out userId);
    }
}