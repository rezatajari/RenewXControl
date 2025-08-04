using API.Utility;
using Application.Interfaces.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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
        var uploadedFile = new FormFileAdapter(file);
        var result = await _fileService.SaveProfileImageAsync(uploadedFile);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

}