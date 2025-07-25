﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Application.DTOs.AddAsset;

namespace RenewXControl.Api.Controllers
{
    /// <summary>
    /// Provides endpoints for managing user sites.
    /// </summary>
    [Authorize]
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
        [HttpPost(template: "add")]
        public async Task<IActionResult> Add([FromBody] AddSite addSite)
        {
            var response = await _siteService.AddSiteAsync(addSite, UserId);
            return (response.IsSuccess) ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Checks if the current user has a site associated with their account
        /// </summary>
        /// <remarks>
        /// This endpoint verifies whether the authenticated user has a site linked to their profile.
        /// The response indicates if a site exists and provides relevant site information if available.
        /// </remarks>
        /// <param name="addSite">Site information payload (currently unused in the implementation but required for the request body)</param>
        /// <response code="200">Returns success status with site information if the user has a site</response>
        /// <response code="400">Returns error information if the request fails</response>
        [HttpGet(template: "HasSite")]
        public async Task<IActionResult> HasSite()
        {
            var response = await _siteService.HasSiteAsync(UserId);
            return (response.IsSuccess) ? Ok(response) : BadRequest(response);
        }
    }
}
