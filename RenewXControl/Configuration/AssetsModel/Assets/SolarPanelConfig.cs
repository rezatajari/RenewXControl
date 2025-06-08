namespace RenewXControl.Configuration.AssetsModel.Assets;

public record SolarPanelConfig
{
    public double Irradiance { get; init; }
    public double SetPoint { get; init; }
    public double ActivePower { get; init; }
}