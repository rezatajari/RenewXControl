namespace Application.DTOs.AddAsset;

public record AddSolar
{
    public double Irradiance { get; init; }
    public double SetPoint { get; init; }
    public double ActivePower { get; init; }
}