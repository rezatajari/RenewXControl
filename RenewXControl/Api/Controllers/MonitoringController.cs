using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RenewXControl.Application.Asset.Interfaces;

namespace RenewXControl.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        private readonly IAssetService _assetService;

        public MonitoringController(IAssetService assetService)
        {
                _assetService=assetService;
        }

        [HttpPost("register-assets")]
        public async Task<IActionResult> DataAsset()
        {
            var userId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           await _assetService.MonitoringData(userId);

           return Ok();
        }
    }
}
