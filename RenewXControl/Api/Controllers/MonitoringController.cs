using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RenewXControl.Application.Asset.Interfaces.Monitoring;

namespace RenewXControl.Api.Controllers
{
    /// <summary>
    /// Provides endpoints for monitoring operations such as registering monitoring sessions.
    /// </summary>
    [Authorize]
    [Route(template: "api/[controller]")]
    [ApiController]
    public class MonitoringController : BaseController
    {
        private readonly IMonitoringService _monitoringService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitoringController"/> class.
        /// </summary>
        /// <param name="monitoringService">The monitoring service instance.</param>
        public MonitoringController(IMonitoringService monitoringService)
        {
            _monitoringService = monitoringService;
        }

        /// <summary>
        /// Registers a new monitoring session for the authenticated user.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the registration operation.</returns>
        /// <response code="200">Monitoring session registered successfully.</response>
        /// <response code="400">Registration failed due to invalid data or other issues.</response>
        [HttpPost(template: "register")]
        public async Task<IActionResult> Register()
        {
            await _monitoringService.RegisterMonitoringSession(UserId);
            return Ok();
        }
    }
}
