using Domain.Entities.Assets;
using Domain.Interfaces.Assets;

namespace Domain.Implementations.Assets;

public class SolarControl(SolarPanel panel) : ISolarControl
{
    public string StatusMessage { get; set; } = string.Empty;

    public bool Start()
    {
        if (panel.Start())
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
    public bool Stop()
    {
        panel.Stop();
        StatusMessage = "Solar panel is off";
        return true;
    }
    public bool UpdateIrradiance()
    {
        if (panel.UpdateIrradiance())
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
    public double Irradiance => panel.Irradiance; 
    public void UpdateActivePower()
    {
        panel.UpdateActivePower();
    }
    public double ActivePower=> panel.ActivePower;
    public void RecalculateSetPoint()
    {
        panel.UpdateSetPoint();
    }

    public double SetPoint => panel.SetPoint;
}