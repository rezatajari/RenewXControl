using Application.Interfaces.User;

namespace API.Utility;

public class FormFileAdapter(IFormFile formFile) : IUploadedFile
{

    public string FileName => formFile.FileName;
    public string ContentType => formFile.ContentType;
    public long Length => formFile.Length;
    public Stream OpenReadStream() => formFile.OpenReadStream();
}