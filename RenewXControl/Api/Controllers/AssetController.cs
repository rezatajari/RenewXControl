using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Application.DTOs.AddAsset;

namespace RenewXControl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;

        public AssetController(IAssetService assetService)
        {
            _assetService=assetService; 
        }

        [HttpPost("add/battery")]
        public async Task<IActionResult> AddBattery(AddBattery addBattery, string userId)
        {
            var result = await _assetService.AddBatteryAsync(addBattery, userId);
            return (result.IsSuccess) ? Ok(result) : BadRequest(result);
        }

        [HttpPost("add/solar")]
        public async Task<IActionResult> AddSolar(AddSolar addSolar, string userId)
        {
            var result = await _assetService.AddSolarAsync(addSolar, userId);
            return (result.IsSuccess) ? Ok(result):BadRequest(result);
        }

        [HttpPost("add/turbine")]
        public async Task<IActionResult> AddTurbine(AddTurbine addTurbine, string userId)
        {
            var result = await _assetService.AddTurbineAsync(addTurbine, userId);
            return (result.IsSuccess) ? Ok(result) : BadRequest(result);
        }
    }
}
