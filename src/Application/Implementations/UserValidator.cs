using Application.Common;
using Application.Interfaces.User;

namespace Application.Implementations;

public class UserValidator:IUserValidator
{
    public GeneralResponse<bool> ValidateUserId(Guid userId)
    {
        if (userId==Guid.Empty)
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