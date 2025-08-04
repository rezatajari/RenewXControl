namespace Domain.Entities.Assets;

public class WindTurbine : Asset
{
    private WindTurbine() { }
    private WindTurbine(double windSpeed, double activePower, double setPoint, Guid siteId)
    {
        Name = $"WT{Id}";
        WindSpeed = windSpeed;
        ActivePower = activePower;
        SetPoint = setPoint;
        SiteId = siteId;
    }

    public double WindSpeed { get; private set; }  // km/h
    public double ActivePower { get; private set; } // kW
    public double SetPoint { get; private set; } // Determines operation status

    public static WindTurbine Create(double windSpeed,double activePower,double setPoint, Guid siteId)
        => new WindTurbine(windSpeed, activePower, setPoint,siteId);
    public bool Start()
    {
        SetPoint = 10;
        if (WindSpeed != 0.0 )
        {
            return true;
        }
        else
        {
            ActivePower = 0;
            return false;
        }
    }
    public void Stop()
    {
        ActivePower = SetPoint;
    }

    public void UpdateSetPoint()
    {
        SetPoint = 0;
    }
    public bool UpdateWindSpeed()
    {
        WindSpeed = new Random().NextDouble() * 10;
        return WindSpeed != 0.0 || SetPoint != 0.0;
    }
    public void UpdateActivePower()
    {
        ActivePower = Math.Min(WindSpeed, SetPoint); // Assuming SetPoint is the maximum power output
    }
}