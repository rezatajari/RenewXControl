using Application.DTOs.AddAsset;
using FluentValidation;

namespace Application.Validation.AddAsset;

public sealed class AddTurbineValidator : AbstractValidator<AddTurbine>
{
    public AddTurbineValidator()
    {
        RuleFor(x => x.WindSpeed)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.SetPoint)
            .InclusiveBetween(0, 100);

        RuleFor(x => x.ActivePower)
            .GreaterThanOrEqualTo(0);
    }
}