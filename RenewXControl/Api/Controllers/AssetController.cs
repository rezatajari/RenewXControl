using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RenewXControl.Application;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Application.DTOs.AddAsset;

namespace RenewXControl.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;
        private readonly ISiteRepository _siteRepository;

        public AssetController(IAssetService assetService,ISiteRepository siteRepository)
        {
            _assetService=assetService; 
            _siteRepository=siteRepository;
        }

        [HttpPost("add/site")]
        public async Task<IActionResult> AddSite([FromBody] AddSite addSite)
        {
            var userId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _assetService.AddSiteAsync(addSite, userId);
            return (result.IsSuccess)? Ok(result) : BadRequest(result);
        }

        [HttpPost("add/battery")]
        public async Task<IActionResult> AddBattery([FromBody]AddBattery addBattery)
        {
            var userId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var siteId = await _siteRepository.GetSiteIdByUserIdAsync(userId);

            var result = await _assetService.AddBatteryAsync(addBattery,siteId);
            return (result.IsSuccess) ? Ok(result) : BadRequest(result);
        }

        [HttpPost("add/solar")]
        public async Task<IActionResult> AddSolar([FromBody] AddSolar addSolar)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var siteId = await _siteRepository.GetSiteIdByUserIdAsync(userId);

            var result = await _assetService.AddSolarAsync(addSolar, siteId);
            return (result.IsSuccess) ? Ok(result):BadRequest(result);
        }

        [HttpPost("add/turbine")]
        public async Task<IActionResult> AddTurbine([FromBody] AddTurbine addTurbine)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var siteId = await _siteRepository.GetSiteIdByUserIdAsync(userId);

            var result = await _assetService.AddTurbineAsync(addTurbine, siteId);
            return (result.IsSuccess) ? Ok(result) : BadRequest(result);
        }
    }
}
