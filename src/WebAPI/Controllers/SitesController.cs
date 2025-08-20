using Application.DTOs.AddAsset;
using Application.Interfaces.Asset;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Provides endpoints for managing user sites.
/// </summary>
[Authorize]
[Route(template: "[controller]")]
[ApiController]
public class SitesController : BaseController
{
    private readonly ISiteService _siteService;

    /// <summary>
    /// Initializes a new instance of the <see cref="SitesController"/> class.
    /// </summary>
    /// <param name="siteService">The site service instance.</param>
    public SitesController(ISiteService siteService)
    {
        _siteService = siteService;
    }


    [HttpGet]
    public async Task<IActionResult> GetSites()
    {
        var response= await _siteService.GetSites(UserId);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// Adds a new site for the authenticated user.
    /// </summary>
    /// <param name="addSite">The site data to add.</param>
    /// <returns>A response indicating the result of the add operation.</returns>
    /// <response code="200">Site added successfully.</response>
    /// <response code="400">Invalid data or operation failed.</response>
    [HttpPost(template: "Site")]
    public async Task<IActionResult> Add([FromBody] AddSite addSite)
    {
        var response = await _siteService.AddSite(addSite, UserId);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
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
    [HttpGet(template: "User/UserId/Has-Site")]
    public async Task<IActionResult> HasSite()
    {
        var response = await _siteService.HasSite(UserId);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}