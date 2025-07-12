using RenewXControl.Api.Utility;
using RenewXControl.Application.Asset.Interfaces;

namespace RenewXControl.Application.Asset.Implementation;

public class UserValidator:IUserValidator
{
    public GeneralResponse<bool> ValidateUserId(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return GeneralResponse<bool>.Failure(
                message: "User detection failed",
                errors: [
                    new ErrorResponse { Name = "User Credential", Message = "User ID is required." }
                ]);
        }

        return GeneralResponse<bool>.Success(true);
    }
}