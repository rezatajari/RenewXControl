namespace Domain.Entities.Assets;

public class SolarPanel : Asset
{
    private SolarPanel() { }
    private SolarPanel(double irradiance, double activePower, double setPoint,Guid siteId)
    {
        Name = $"SP{Id}";
        Irradiance = irradiance;
        ActivePower = activePower;
        SetPoint = setPoint;
        SiteId = siteId;
    }

    public double Irradiance { get; private set; } // W/m²
    public double ActivePower { get; private set; } // kW
    public double SetPoint { get; private set; }  // Determines operation status
    public string StatusMessage { get; private set; } 

    public static SolarPanel Create(double irradiance, double activePower,double setPoint, Guid siteId)
        => new SolarPanel(irradiance, activePower, setPoint,siteId);

    public bool Start()
    {

        SetPoint = 10;
        if (Irradiance != 0.0)
        {
            StatusMessage = "Solar panel is generating power";
            return true;
        }
        else
        {
            ActivePower = 0;
            StatusMessage = "Solar panel is not generating power";
            return false;
        }
    }
    public bool Stop()
    {
        ActivePower = SetPoint;
        StatusMessage = "Solar panel is off";
        return true;
    }

    public void UpdateSetPoint()
    {
        SetPoint = 0;
    }
    public bool UpdateIrradiance()
    {
        Irradiance = new Random().NextDouble() * 10;
        if (Irradiance != 0.0 || SetPoint != 0.0)
        {
            StatusMessage = "Solar panel is generating power";
            return true;
        }
        else
        {
            StatusMessage = "Solar panel is not generating power";
            return false;
        }
    }
    public void UpdateActivePower()
    {
        ActivePower = Math.Min(Irradiance, SetPoint); // Assuming SetPoint is the maximum power output
    }
}