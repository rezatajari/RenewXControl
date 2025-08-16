namespace WebClient.DTOs;

public class GeneralResponse<T> 
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public T Data { get;  set; }
    public List<ErrorResponse> Errors { get; set; } = new();
}