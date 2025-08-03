using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RenewXControl.Application;

namespace RenewXControl.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService= fileService;
        }

        [HttpPost(template: "Upload/ProfileImage")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var result = await _fileService.SaveProfileImageAsync(file);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

    }
}
