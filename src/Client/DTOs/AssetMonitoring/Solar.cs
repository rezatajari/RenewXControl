using RXC.Client.DTOs;


public record Solar:Asset
{
    public double Irradiance { get; set; }
    public double ActivePower { get; set; }
}
    
    
