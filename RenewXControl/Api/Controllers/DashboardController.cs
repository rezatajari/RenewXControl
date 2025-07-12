using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RenewXControl.Application.User.Interfaces;

namespace RenewXControl.Api.Controllers
{
    [Authorize]
    [Route(template:"api/[controller]")]
    [ApiController]
    public class DashboardController : BaseController
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var response = await _dashboardService.GetProfile(UserId);
            return Ok(response);
        }
    }
}
