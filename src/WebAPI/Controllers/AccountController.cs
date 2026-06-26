using Application.DTOs.User.Auth;
using Application.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Handles authentication operations such as user registration and login.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="AccountController"/> class.
/// </remarks>
/// <param name="accountService">The authentication service.</param>
[Route(template: "[controller]")]
[ApiController]
public class AccountController(IAccountService accountService) : BaseController
{
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="register">The registration data.</param>
    /// <returns>A response indicating the result of the registration operation.</returns>
    /// <response code="200">Registration successful.</response>
    /// <response code="400">Registration failed due to validation errors or other issues.</response>
    [HttpPost(template: "Register")]
    public async Task<IActionResult> Register([FromBody] Register register)
    {
        var result = await accountService.RegisterAsync(register);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Authenticates a user and returns a JWT token if successful.
    /// </summary>
    /// <param name="login">The login credentials.</param>
    /// <returns>A response containing the authentication token if successful.</returns>
    /// <response code="200">Login successful.</response>
    /// <response code="400">Login failed due to invalid credentials or other issues.</response>
    [HttpPost(template: "Login")]
    public async Task<IActionResult> Login([FromBody] Login login)
    {
        var result = await accountService.LoginAsync(login);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost(template: "Logout")]
    public async Task<IActionResult> Logout()
    {
        var result = await accountService.LogoutAsync();
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}