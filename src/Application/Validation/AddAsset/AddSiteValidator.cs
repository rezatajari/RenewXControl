using Application.DTOs.AddAsset;
using FluentValidation;

namespace Application.Validation.AddAsset;

public sealed class AddSiteValidator : AbstractValidator<AddSite>
{
    public AddSiteValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Location)
            .NotEmpty()
            .MaximumLength(100);
    }
}