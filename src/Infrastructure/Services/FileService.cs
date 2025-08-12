using Application.Common;
using Application.Interfaces.File;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Services;

public class FileService:IFileService
{
    private readonly IWebHostEnvironment _env;

    public FileService(IWebHostEnvironment env)
    {
        _env=env;
    }

    public async Task<GeneralResponse<string>> SaveProfileImageAsync(IUploadedFile file)
    {
        if (file == null || file.Length == 0)
            return GeneralResponse<string>.Failure("File is missing or empty.");

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        var fileExtension = Path.GetExtension(file.FileName).ToLower();

        if (!allowedExtensions.Contains(fileExtension))
            return GeneralResponse<string>.Failure("Invalid file type. Only images are allowed.");

        if (file.Length > 5_000_000) // 5MB limit
            return GeneralResponse<string>.Failure("File size exceeds 5MB limit.");

        try
        {
            // Use wwwroot/profile-images for better organization
            var uploadFolder = Path.Combine(_env.WebRootPath, "profile-images");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadFolder, uniqueFileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.OpenReadStream().CopyToAsync(stream);

            // Return path relative to wwwroot
            return GeneralResponse<string>.Success($"profile-images/{uniqueFileName}");
        }
        catch (Exception ex)
        {
            return GeneralResponse<string>.Failure("File upload failed. Please try again.");
        }
    }

}