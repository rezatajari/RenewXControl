using RXC.Client.DTOs;
public record Asset
{
    public string AssetType { get; set; }
    public string Message { get; set; }
    public double SetPoint { get; set; }
    public DateTime TimeStamp { get; set; }
}
