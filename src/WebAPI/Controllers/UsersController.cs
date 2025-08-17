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
[Route(template: "[controller]/User")]
[ApiController]
public class UsersController : BaseController
{
    private readonly IUsersService _usersService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UsersController"/> class.
    /// </summary>
    /// <param name="dashboardService">The dashboard service instance.</param>
    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    /// <summary>
    /// Gets the profile information for the currently authenticated user.
    /// </summary>
    /// <returns>A <see cref="GeneralResponse{T}"/> containing the user's profile data.</returns>
    /// <response code="200">Returns the user's profile information.</response>
    /// <response code="401">If the user is not authenticated.</response>
    [HttpGet(template:"Profile")]
    public async Task<IActionResult> Profile()
    {
        var result = await _usersService.GetProfile(UserId);
        return Ok(result);
    }

    [HttpPut(template: "Profile/Edit")]
    public async Task<IActionResult> EditProfile([FromBody] EditProfile editProfile)
    {
        var result = await _usersService.EditProfile(editProfile,UserId);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }


    [HttpPut(template: "Change-Password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePassword changePassword)
    {

        var result = await _usersService.ChangePasswordAsync(changePassword, UserId);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }


    [HttpPost(template: "Upload/Profile-Image")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        var uploadedFile = new FormFileAdapter(file);
        var result = await _usersService.SaveProfileImageAsync(uploadedFile);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}