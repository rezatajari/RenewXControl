using Microsoft.AspNetCore.Mvc;
using RenewXControl.Application.DTOs.User.Auth;
using RenewXControl.Application.User;

namespace RenewXControl.Api.Controllers
{
    [Route(template:"api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost(template:"register")]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            var result = await _authService.RegisterAsync(register);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost(template:"login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var result = await _authService.LoginAsync(login);
            return result.IsSuccess? Ok(result) : BadRequest(result);
        }

    }
}
