using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Application.DTOs.AddAsset;

namespace RenewXControl.Api.Controllers
{
    [Route(template:"api/[controller]")]
    [ApiController]
    public class SiteController : BaseController
    {
        private readonly ISiteService _siteService;

        public SiteController( ISiteService siteService)
        {
            _siteService = siteService;
        }

        [HttpPost(template: "add/site")]
        public async Task<IActionResult> AddSite([FromBody] AddSite addSite)
        {
            var response = await _siteService.AddSiteAsync(addSite, UserId);
            return (response.IsSuccess) ? Ok(response) : BadRequest(response);
        }
    }
}
