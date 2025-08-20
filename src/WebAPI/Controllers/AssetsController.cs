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
    [Route(template: "Sites/{siteId:guid}/[controller]")]
    [ApiController]
    public class AssetsController : BaseController
    {
        private readonly IAssetService _assetService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetsController"/> class.
        /// </summary>
        /// <param name="assetService">The asset service instance.</param>
        public AssetsController(IAssetService assetService)
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
        [HttpPost(template: "battery")]
        public async Task<IActionResult> AddBattery([FromBody] AddBattery addBattery, Guid siteId)
        {

            var response = await _assetService.AddBattery(UserId, addBattery, siteId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Adds a new solar panel asset for the authenticated user.
        /// </summary>
        /// <param name="addSolar">The solar panel asset data.</param>
        /// <returns>A response indicating the result of the add operation.</returns>
        /// <response code="200">Solar panel added successfully.</response>
        /// <response code="400">Invalid data or operation failed.</response>
        [HttpPost(template: "solar")]
        public async Task<IActionResult> AddSolar([FromBody] AddSolar addSolar, Guid siteId)
        {
            var response = await _assetService.AddSolar(UserId,addSolar,siteId );
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Adds a new wind turbine asset for the authenticated user.
        /// </summary>
        /// <param name="addTurbine">The wind turbine asset data.</param>
        /// <returns>A response indicating the result of the add operation.</returns>
        /// <response code="200">Wind turbine added successfully.</response>
        /// <response code="400">Invalid data or operation failed.</response>
        [HttpPost(template: "turbine")]
        public async Task<IActionResult> AddTurbine([FromBody] AddTurbine addTurbine, Guid siteId)
        {
            var response = await _assetService.AddTurbine(UserId, addTurbine, siteId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
