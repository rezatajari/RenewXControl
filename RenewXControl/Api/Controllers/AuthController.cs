using Microsoft.AspNetCore.Mvc;
using RenewXControl.Application.DTOs.User.Auth;
using RenewXControl.Application.User.Interfaces;

namespace RenewXControl.Api.Controllers
{
    /// <summary>
    /// Handles authentication operations such as user registration and login.
    /// </summary>
    [Route(template: "api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">The authentication service.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="register">The registration data.</param>
        /// <returns>A response indicating the result of the registration operation.</returns>
        /// <response code="200">Registration successful.</response>
        /// <response code="400">Registration failed due to validation errors or other issues.</response>
        [HttpPost(template: "register")]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            var result = await _authService.RegisterAsync(register);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token if successful.
        /// </summary>
        /// <param name="login">The login credentials.</param>
        /// <returns>A response containing the authentication token if successful.</returns>
        /// <response code="200">Login successful.</response>
        /// <response code="400">Login failed due to invalid credentials or other issues.</response>
        [HttpPost(template: "login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var result = await _authService.LoginAsync(login);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost(template: "logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _authService.LogoutAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
