using System.ComponentModel.DataAnnotations;
namespace RenewXControl.Application.DTOs.AddAsset;

public record AddBattery
{
    [Range(1, double.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
    public double Capacity { get; set; }

    [Range(0, 100, ErrorMessage = "StateCharge must be between 0 and 100")]
    public double StateCharge { get; set; }

    [Range(0, 100, ErrorMessage = "SetPoint must be between 0 and 100")]
    public double SetPoint { get; set; }

    [Range(0, 100, ErrorMessage = "FrequentlyDischarge must be between 0 and 100")]
    public double FrequentlyDischarge { get; set; }
}
