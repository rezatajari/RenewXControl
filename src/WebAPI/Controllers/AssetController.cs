using Application.DTOs.AddAsset;
using Application.Interfaces.Asset;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Provides endpoints for managing user assets such as batteries, solar panels, and wind turbines.
    /// </summary>
    [Authorize]
    [Route(template: "api/[controller]")]
    [ApiController]
    public class AssetController : BaseController
    {
        private readonly IAssetService _assetService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetController"/> class.
        /// </summary>
        /// <param name="assetService">The asset service instance.</param>
        public AssetController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        /// <summary>
        /// Adds a new battery asset for the authenticated user.
        /// </summary>
        /// <param name="addBattery">The battery asset data.</param>
        /// <returns>A response indicating the result of the add operation.</returns>
        /// <response code="200">Battery added successfully.</response>
        /// <response code="400">Invalid data or operation failed.</response>
        [HttpPost(template: "add/battery")]
        public async Task<IActionResult> AddBattery([FromBody] AddBattery addBattery)
        {

            var response = await _assetService.AddBatteryAsync(addBattery, UserId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Adds a new solar panel asset for the authenticated user.
        /// </summary>
        /// <param name="addSolar">The solar panel asset data.</param>
        /// <returns>A response indicating the result of the add operation.</returns>
        /// <response code="200">Solar panel added successfully.</response>
        /// <response code="400">Invalid data or operation failed.</response>
        [HttpPost(template: "add/solar")]
        public async Task<IActionResult> AddSolar([FromBody] AddSolar addSolar)
        {
            var response = await _assetService.AddSolarAsync(addSolar, UserId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Adds a new wind turbine asset for the authenticated user.
        /// </summary>
        /// <param name="addTurbine">The wind turbine asset data.</param>
        /// <returns>A response indicating the result of the add operation.</returns>
        /// <response code="200">Wind turbine added successfully.</response>
        /// <response code="400">Invalid data or operation failed.</response>
        [HttpPost(template: "add/turbine")]
        public async Task<IActionResult> AddTurbine([FromBody] AddTurbine addTurbine)
        {
            var response = await _assetService.AddTurbineAsync(addTurbine, UserId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
