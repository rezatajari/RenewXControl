using API.Utility;
using Application.Common;
using Application.DTOs.User.Profile;
using Application.Interfaces.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Provides endpoints for user dashboard operations.
/// </summary>
[Authorize]
[Route(template: "api/[controller]")]
[ApiController]
public class DashboardController : BaseController
{
    private readonly IDashboardService _dashboardService;

    /// <summary>
    /// Initializes a new instance of the <see cref="DashboardController"/> class.
    /// </summary>
    /// <param name="dashboardService">The dashboard service instance.</param>
    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    /// <summary>
    /// Gets the profile information for the currently authenticated user.
    /// </summary>
    /// <returns>A <see cref="GeneralResponse{T}"/> containing the user's profile data.</returns>
    /// <response code="200">Returns the user's profile information.</response>
    /// <response code="401">If the user is not authenticated.</response>
    [HttpGet(template:"profile")]
    public async Task<IActionResult> Profile()
    {
        var result = await _dashboardService.GetProfile(UserId);
        return Ok(result);
    }

    [HttpPut(template: "Profile/Edit")]
    public async Task<IActionResult> EditProfile([FromBody] EditProfile editProfile)
    {
        var result = await _dashboardService.EditProfile(editProfile,UserId);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}