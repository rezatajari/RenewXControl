using RenewXControl.Api.Utility;
using RenewXControl.Configuration.AssetsModel.Assets;
using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Domain.Assets
{
    public class WindTurbine : Asset
    {
        private WindTurbine() { }
        private WindTurbine(double windSpeed, double activePower, double setPoint)
        {
            Name = $"WT{Id}";
            WindSpeed = windSpeed;
            ActivePower = activePower;
            SetPoint = setPoint;
        }

        public double WindSpeed { get; private set; }  // km/h
        public double ActivePower { get; private set; } // kW
        public double SetPoint { get; private set; } // Determines operation status

        public static WindTurbine Create(WindTurbineConfig turbineConfig)
            => new WindTurbine(turbineConfig.WindSpeed, turbineConfig.ActivePower, turbineConfig.SetPoint);
        public bool Start()
        {
            if (WindSpeed != 0.0 && SetPoint != 0.0)
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
            SetPoint = 0;
            ActivePower = SetPoint;
        }

        public void UpdateSetPoint(double amount)
        {
            SetPoint = amount; // new Random().NextDouble() * WindSpeed;
            if (SetPoint != 0)
            {
                ActivePower = SetPoint;
            }
            else
            {
                Stop();
            }
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
}
