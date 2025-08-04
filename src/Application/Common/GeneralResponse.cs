namespace Application.Common;

public class GeneralResponse<T> 
{
    // For success
    private GeneralResponse(T data, string message, bool isSuccess)
    {
        Data = data;
        Message = message;
        IsSuccess = isSuccess;
    }

    // For failure
    private GeneralResponse(string message, bool isSuccess,List<ErrorResponse> errors)
    {
        Message = message;
        IsSuccess = isSuccess;
        Errors = errors;
    }
    public bool IsSuccess { get;private set; }
    public string Message { get;private set; }
    public T Data { get; private set; }
    public List<ErrorResponse> Errors { get; private set; }

    public static GeneralResponse<T> Success(T data, string message = "Operation successful")
        => new GeneralResponse<T>(data, message, isSuccess:true);

    public static GeneralResponse<T> Failure(string message = "Operation failed",List<ErrorResponse> errors=null)
        => new GeneralResponse<T>(message,isSuccess:false,errors);
}