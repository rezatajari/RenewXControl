using Application.Common;

namespace API.Utility;

public class GlobalErrorException
{
    private readonly RequestDelegate _next;

    public GlobalErrorException(RequestDelegate next)
    {
        _next=next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnauthorizedAccessException e)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var response = GeneralResponse<string>.Failure(
                message: "Unauthorized access.",
                errors: [
                    new ErrorResponse
                    {
                        Name = "Unauthorized",
                        Message = e.Message
                    }]
            );

            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception e)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";


            var response = GeneralResponse<string>.Failure(
                message: "An unexpected error occurred",
                errors: [
                    new ErrorResponse
                    {
                        Name = "Exception",
                        Message = e.Message
                    }]);

            await context.Response.WriteAsJsonAsync(response);

        }
    }

}