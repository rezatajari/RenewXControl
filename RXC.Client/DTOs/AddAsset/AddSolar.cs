using System.ComponentModel.DataAnnotations;

namespace RXC.Client.DTOs.AddAsset;

public record AddSolar
{
    [Range(0, double.MaxValue, ErrorMessage = "Irradiance must be non-negative")]
    public double Irradiance { get; set; }

    [Range(0, 100, ErrorMessage = "SetPoint must be between 0 and 100")]
    public double SetPoint { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "ActivePower must be non-negative")]
    public double ActivePower { get; set; }
}