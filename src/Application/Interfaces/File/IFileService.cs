using Application.Common;

namespace Application.Interfaces.File;

public interface IFileService
{
    Task<GeneralResponse<string>> SaveProfileImageAsync(IUploadedFile file);
}