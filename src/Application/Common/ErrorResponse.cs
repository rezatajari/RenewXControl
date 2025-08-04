namespace Application.Common;

public class ErrorResponse
{
    public ErrorResponse()
    {
    }

    public ErrorResponse(string message, string name)
    {
        Message = message;
        Name = name;
    }

    public string Name { get; set; }    // E.g. "Password"
    public string Message{ get; set; }    // E.g. "Password is too short"
}