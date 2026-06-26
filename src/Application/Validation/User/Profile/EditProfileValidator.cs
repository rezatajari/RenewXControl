using Application.DTOs.User.Profile;
using FluentValidation;

namespace Application.Validation.User.Profile;

public sealed class EditProfileValidator : AbstractValidator<EditProfile>
{
    public EditProfileValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50)
            .Matches(@"^[a-zA-Z0-9_\-\.]+$");

        RuleFor(x => x.ProfileImage)
            .MaximumLength(2048)
            .When(x => !string.IsNullOrWhiteSpace(x.ProfileImage));
    }
}