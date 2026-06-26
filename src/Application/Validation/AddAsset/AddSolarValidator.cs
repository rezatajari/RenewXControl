using Application.DTOs.AddAsset;
using FluentValidation;

namespace Application.Validation.AddAsset;

public sealed class AddSolarValidator : AbstractValidator<AddSolar>
{
    public AddSolarValidator()
    {
        RuleFor(x => x.Irradiance)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.SetPoint)
            .InclusiveBetween(0, 100);

        RuleFor(x => x.ActivePower)
            .GreaterThanOrEqualTo(0);
    }
}