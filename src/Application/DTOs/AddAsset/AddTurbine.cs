namespace Application.DTOs.AddAsset;

public record AddTurbine
{
    public double WindSpeed { get; init; }
    public double SetPoint { get; init; }
    public double ActivePower { get; init; }
}