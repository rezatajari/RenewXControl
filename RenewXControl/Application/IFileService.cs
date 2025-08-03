using RenewXControl.Api.Utility;

namespace RenewXControl.Application;

public interface IFileService
{
    Task<GeneralResponse<string>> SaveProfileImageAsync(IFormFile file);
}