namespace WebClient.DTOs.AssetMonitoring;

public record Turbine: Asset
{
    public double WindSpeed { get; set; }
    public double ActivePower { get; set; }
    public string Message { get; set; }
}