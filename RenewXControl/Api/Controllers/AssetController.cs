using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RenewXControl.Application;
using RenewXControl.Application.Asset.Interfaces.Asset;
using RenewXControl.Application.DTOs.AddAsset;

namespace RenewXControl.Api.Controllers
{
    [Authorize]
    [Route(template:"api/[controller]")]
    [ApiController]
    public class AssetController : BaseController
    {
        private readonly IAssetService _assetService;

        public AssetController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        [HttpPost(template:"add/battery")]
        public async Task<IActionResult> AddBattery([FromBody]AddBattery addBattery)
        {
            var response = await _assetService.AddBatteryAsync(addBattery,UserId);
            return (response.IsSuccess) ? Ok(response) : BadRequest(response);
        }

        [HttpPost(template:"add/solar")]
        public async Task<IActionResult> AddSolar([FromBody] AddSolar addSolar)
        {
            var response = await _assetService.AddSolarAsync(addSolar, UserId);
            return (response.IsSuccess) ? Ok(response):BadRequest(response);
        }

        [HttpPost(template:"add/turbine")]
        public async Task<IActionResult> AddTurbine([FromBody] AddTurbine addTurbine)
        {
            var response = await _assetService.AddTurbineAsync(addTurbine, UserId);
            return (response.IsSuccess) ? Ok(response) : BadRequest(response);
        }
    }
}
