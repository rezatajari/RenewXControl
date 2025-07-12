using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RenewXControl.Api.Utility;

namespace RenewXControl.Api
{
    public class ValidateModelAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ModelState.IsValid)
            {
               var errors = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors.Select(
                        e => new ErrorResponse
                        {
                            Name = x.Key,
                            Message = e.ErrorMessage
                        }))
                    .ToList();

                var response = GeneralResponse<object>.Failure(
                    message: "Validation failed",
                    errors: errors);

                context.Result = new BadRequestObjectResult(response);
            }
        }
    }
}
