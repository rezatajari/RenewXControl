using RXC.Client.DTOs;

public record Turbine: Asset
{
    public double WindSpeed { get; set; }
    public double ActivePower { get; set; }
}