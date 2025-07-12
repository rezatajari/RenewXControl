using System.ComponentModel.DataAnnotations;

namespace RenewXControl.Application.DTOs.AddAsset;

/// <summary>
/// Data required to add a new solar panel asset.
/// </summary>
public record AddSolar
{
    /// <summary>
    /// The irradiance value for the solar panel (W/m²).
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "Irradiance must be non-negative")]
    public double Irradiance { get; set; }

    /// <summary>
    /// The set point for solar panel operation (0-100%).
    /// </summary>
    [Range(0, 100, ErrorMessage = "SetPoint must be between 0 and 100")]
    public double SetPoint { get; set; }

    /// <summary>
    /// The active power output of the solar panel (kW).
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "ActivePower must be non-negative")]
    public double ActivePower { get; set; }
}
