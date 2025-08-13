namespace WebClient.DTOs.AssetMonitoring;

public record Solar:Asset
{
    public double Irradiance { get; set; }
    public double ActivePower { get; set; }
    public string Message { get; set; }
}