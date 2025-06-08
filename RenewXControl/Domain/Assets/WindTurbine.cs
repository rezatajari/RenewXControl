using RenewXControl.Configuration.AssetsModel.Assets;
using RenewXControl.Domain.Assets.Interfaces;

namespace RenewXControl.Domain.Assets
{
    public class WindTurbine : Asset, ITurbineControl
    {
        private WindTurbine() { }
        private WindTurbine(double windSpeed, double activePower, double setPoint)
        {
            Name = $"WT{Id}";
            WindSpeed = windSpeed;
            ActivePower = activePower;
            SetPoint = setPoint;
            PowerStatusMessage = "Wind turbine is not generating power now";
        }

        public double WindSpeed { get; private set; }  // km/h
        public double ActivePower { get; private set; } // kW
        public double SetPoint { get; private set; } // Determines operation status
        public string PowerStatusMessage { get; set; }

        public static WindTurbine Create(WindTurbineConfig turbineConfig)
            => new WindTurbine(turbineConfig.WindSpeed, turbineConfig.ActivePower, turbineConfig.SetPoint);
        public void Start()
        {
            if (WindSpeed == 0.0 || SetPoint == 0.0)
            {
                ActivePower = 0;
                PowerStatusMessage = "Wind turbine is not generating power";
            }
            else
            {
                PowerStatusMessage = "Wind turbine is generating power";
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

        public void UpdateWindSpeed()
        {
            WindSpeed = new Random().NextDouble() * 10;
            if (WindSpeed == 0.0 || SetPoint == 0.0)
            {
                ActivePower = 0;
                PowerStatusMessage = "Wind turbine is not generating power";
            }
            else
            {
                PowerStatusMessage = "Wind turbine is generating power";
            }
        }
        public void UpdateActivePower()
        {
            ActivePower = Math.Min(WindSpeed, SetPoint); // Assuming SetPoint is the maximum power output
        }
    }
}
