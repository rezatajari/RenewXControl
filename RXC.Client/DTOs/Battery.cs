using RXC.Client.DTOs;

public record Battery:Asset
{
    public double Capacity { get; set; }
    public double StateCharge { get; set; }
    public double RateDischarge { get; set; }
}