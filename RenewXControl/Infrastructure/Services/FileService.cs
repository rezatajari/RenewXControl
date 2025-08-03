using RenewXControl.Api.Utility;
using RenewXControl.Application;

namespace RenewXControl.Infrastructure.Services;

public class FileService:IFileService
{
    private readonly IWebHostEnvironment _env;

    public FileService(IWebHostEnvironment env)
    {
        _env=env;
    }

    public async Task<GeneralResponse<string>> SaveProfileImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return GeneralResponse<string>.Failure(
                message: "File is missing or empty.",
                errors: [new ErrorResponse("File", "The uploaded file is empty or null.")]);


        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var fileExtension = Path.GetExtension(file.FileName).ToLower();

        if (!allowedExtensions.Contains(fileExtension))
            return GeneralResponse<string>.Failure(
                message: "Invalid file type.",
                errors: [new ErrorResponse("File", "Only JPG, JPEG, PNG, and GIF are allowed.")]);


        try
        {
            var uploadFolder = Path.Combine(_env.WebRootPath, "profile-images");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var uniqueFileName = Guid.NewGuid() + fileExtension;
            var filePath = Path.Combine(uploadFolder, uniqueFileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return GeneralResponse<string>.Success(
                data: $"profile-images/{uniqueFileName}",
                message: "File uploaded successfully.");
        }
        catch (Exception ex)
        {
            return GeneralResponse<string>.Failure(
                message: "File upload failed.",
                errors: [new ErrorResponse("File", ex.Message)]);
        }
    }

}