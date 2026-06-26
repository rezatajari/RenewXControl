namespace Application.DTOs.AddAsset;

public record AddBattery
{
    public double Capacity { get; init; }
    public double StateCharge { get; init; }
    public double SetPoint { get; init; }
    public double FrequentlyDischarge { get; init; }
}