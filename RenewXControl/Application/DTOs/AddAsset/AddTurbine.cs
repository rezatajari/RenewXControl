using System.ComponentModel.DataAnnotations;

namespace RenewXControl.Application.DTOs.AddAsset;

public record AddTurbine
{
    [Range(0, double.MaxValue, ErrorMessage = "WindSpeed must be non-negative")]
    public double WindSpeed { get; set; }

    [Range(0, 100, ErrorMessage = "SetPoint must be between 0 and 100")]
    public double SetPoint { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "ActivePower must be non-negative")]
    public double ActivePower { get; set; }
}
