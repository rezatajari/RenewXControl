using Application.Interfaces.User;

namespace API.Utility;

public class FormFileAdapter: IUploadedFile
{
    private readonly IFormFile _formFile;

    public FormFileAdapter(IFormFile formFile)
    {
        _formFile = formFile;
    }

    public string FileName => _formFile.FileName;
    public string ContentType => _formFile.ContentType;
    public long Length => _formFile.Length;
    public Stream OpenReadStream() => _formFile.OpenReadStream();
}