using System.ComponentModel.DataAnnotations;

namespace RenewXControl.Application.DTOs.AddAsset;

/// <summary>
/// Data required to add a new wind turbine asset.
/// </summary>
public record AddTurbine
{
    /// <summary>
    /// The wind speed for the turbine (km/h).
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "WindSpeed must be non-negative")]
    public double WindSpeed { get; set; }

    /// <summary>
    /// The set point for turbine operation (0-100%).
    /// </summary>
    [Range(0, 100, ErrorMessage = "SetPoint must be between 0 and 100")]
    public double SetPoint { get; set; }

    /// <summary>
    /// The active power output of the turbine (kW).
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "ActivePower must be non-negative")]
    public double ActivePower { get; set; }
}
