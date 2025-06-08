using Microsoft.Extensions.Logging;
using RenewXControl.Configuration.AssetsModel.Assets;
using RenewXControl.Domain.Assets.Interfaces;

namespace RenewXControl.Domain.Assets
{
    public class SolarPanel : Asset, ISolarControl
    {
        private SolarPanel() { }
        private SolarPanel(double irradiance, double activePower, double setPoint)
        {
            Name = $"SP{Id}";
            Irradiance = irradiance;
            ActivePower = activePower;
            SetPoint = setPoint;
            PowerStatusMessage = "Solar panel is not generating power now";
        }

        public double Irradiance { get; private set; } // W/m²
        public double ActivePower { get; private set; } // kW
        public double SetPoint { get; private set; }  // Determines operation status
        public string PowerStatusMessage { get; set; }

        public static SolarPanel Create(SolarPanelConfig solarConfig)
            => new SolarPanel(solarConfig.Irradiance, solarConfig.ActivePower, solarConfig.SetPoint);

        public void Start()
        {
            if (Irradiance == 0.0 || SetPoint == 0.0)
            {
                ActivePower = 0;
                PowerStatusMessage = "Solar panel is not generating power";
            }
            else
            {
                PowerStatusMessage = "Solar panel is generating power";
            }

        }
        public void Stop()
        {
            SetPoint = 0;
            ActivePower = SetPoint;
        }

        public void UpdateSetPoint(double amount)
        {
            SetPoint = amount; // new Random().NextDouble() * Irradiance;
            if (SetPoint != 0)
            {
                ActivePower = SetPoint;
            }
            else
            {
                Stop();
            }
        }

        public void UpdateIrradiance()
        {
            Irradiance = new Random().NextDouble() * 10;
            if (Irradiance == 0.0 || SetPoint == 0.0)
            {
                PowerStatusMessage = "Solar panel is not generating power";
            }
            else
            {
                PowerStatusMessage = "Solar panel is generating power";
            }
        }
        public void UpdateActivePower()
        {
            ActivePower = Math.Min(Irradiance, SetPoint); // Assuming SetPoint is the maximum power output
        }
    }
}
