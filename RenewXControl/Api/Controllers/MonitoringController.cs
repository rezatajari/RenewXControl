using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RenewXControl.Application.Asset.Interfaces.Monitoring;

namespace RenewXControl.Api.Controllers
{
    [Authorize]
    [Route(template:"api/[controller]")]
    [ApiController]
    public class MonitoringController : BaseController
    {
        private readonly IMonitoringService _monitoringService;

        public MonitoringController(IMonitoringService monitoringService)
        {
                _monitoringService= monitoringService;
        }

        [HttpPost(template:"register")]
        public async Task<IActionResult> Register()
        {
           await _monitoringService.RegisterMonitoringSession(UserId);

           return Ok();
        }
    }
}
