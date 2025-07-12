using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Application.DTOs.AddAsset;

namespace RenewXControl.Api.Controllers
{
    /// <summary>
    /// Provides endpoints for managing user sites.
    /// </summary>
    [Route(template: "api/[controller]")]
    [ApiController]
    public class SiteController : BaseController
    {
        private readonly ISiteService _siteService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteController"/> class.
        /// </summary>
        /// <param name="siteService">The site service instance.</param>
        public SiteController(ISiteService siteService)
        {
            _siteService = siteService;
        }

        /// <summary>
        /// Adds a new site for the authenticated user.
        /// </summary>
        /// <param name="addSite">The site data to add.</param>
        /// <returns>A response indicating the result of the add operation.</returns>
        /// <response code="200">Site added successfully.</response>
        /// <response code="400">Invalid data or operation failed.</response>
        [HttpPost(template: "add/site")]
        public async Task<IActionResult> AddSite([FromBody] AddSite addSite)
        {
            var response = await _siteService.AddSiteAsync(addSite, UserId);
            return (response.IsSuccess) ? Ok(response) : BadRequest(response);
        }
    }
}
