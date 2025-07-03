using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RenewXControl.Api.DTOs.Auth;
using RenewXControl.Application.Interfaces;
using RenewXControl.Domain.Users;

namespace RenewXControl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Register register)
        {
            var result = await _authService.RegisterAsync(register);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login login)
        {
            var result = await _authService.LoginAsync(login);
            return result.IsSuccess? Ok(result) : BadRequest(result);
        }

    }
}
