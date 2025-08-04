using Domain.Entities.Assets;
using Domain.Interfaces.Assets;

namespace Domain.Implementations.Assets;

public class TurbineControl(WindTurbine turbine) : ITurbineControl
{
    public string StatusMessage { get; set; } = string.Empty;

    public bool Start()
    {
        if (turbine.Start())
        {
            StatusMessage = "Wind turbine is generating power";
            return true;
        }
        else
        {
            StatusMessage = "Wind turbine is not generating power";
            return false;
        }
    }
    public bool Stop()
    {
        turbine.Stop();
        StatusMessage = "Wind turbine is off";
        return true;
    }
    public bool UpdateWindSpeed()
    {
        if (turbine.UpdateWindSpeed())
        {
            StatusMessage = "Wind turbine is generating power";
            return true;
        }
        else
        {
            StatusMessage = "Wind turbine is not generating power";
            return false;
        }
    }
    public double WindSpeed => turbine.WindSpeed;
    public void UpdateActivePower()
    {
        turbine.UpdateActivePower();
    }
    public double ActivePower => turbine.ActivePower;
    public void RecalculateSetPoint()
    {
        turbine.UpdateSetPoint();
    }

    public double SetPoint => turbine.SetPoint;
}