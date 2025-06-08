namespace RenewXControl.Configuration.AssetsModel.Assets;

public record BatteryConfig
{
    public double Capacity { get; init; }
    public double StateCharge { get; init; }
    public double SetPoint { get; init; }
    public double FrequentlyDisCharge { get; init; }
}