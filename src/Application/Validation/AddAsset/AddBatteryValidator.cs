using Application.DTOs.AddAsset;
using FluentValidation;

namespace Application.Validation.AddAsset;

public sealed class AddBatteryValidator : AbstractValidator<AddBattery>
{
    public AddBatteryValidator()
    {
        RuleFor(x => x.Capacity)
            .GreaterThan(0);

        RuleFor(x => x.StateCharge)
            .InclusiveBetween(0, 100);

        RuleFor(x => x.SetPoint)
            .InclusiveBetween(0, 100);

        RuleFor(x => x.FrequentlyDischarge)
            .InclusiveBetween(0, 100);
    }
}