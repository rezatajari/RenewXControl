using System.ComponentModel.DataAnnotations;
namespace RenewXControl.Application.DTOs.AddAsset;

/// <summary>
/// Data required to add a new battery asset.
/// </summary>
public record AddBattery
{
    /// <summary>Battery capacity in kWh.</summary>
    [Range(1, double.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
    public double Capacity { get; init; }

    /// <summary>Initial state of charge (0-100%).</summary>
    [Range(0, 100, ErrorMessage = "StateCharge must be between 0 and 100")]
    public double StateCharge { get; init; }

    /// <summary>Set point for battery operation (0-100%).</summary>
    [Range(0, 100, ErrorMessage = "SetPoint must be between 0 and 100")]
    public double SetPoint { get; init; }

    /// <summary>Frequent discharge rate (0-100%).</summary>
    [Range(0, 100, ErrorMessage = "FrequentlyDischarge must be between 0 and 100")]
    public double FrequentlyDischarge { get; init; }
}
