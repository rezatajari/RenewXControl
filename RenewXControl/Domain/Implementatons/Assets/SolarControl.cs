using RenewXControl.Api.Utility;
using RenewXControl.Domain.Assets;
using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Domain.Implementatons.Assets
{
    public class SolarControl:ISolarControl
    {
        private readonly SolarPanel _panel;
        public SolarControl(SolarPanel panel)
        {
            _panel = panel;
        }

        public string StatusMessage { get; set; }

        public bool Start()
        {
            if (_panel.Start())
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
            _panel.Stop();
            StatusMessage = "Solar panel is off";
            return true;
        }
        public bool UpdateIrradiance()
        {
            if (_panel.UpdateIrradiance())
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
        public double Irradiance => _panel.Irradiance; 
        public void UpdateActivePower()
        {
           _panel.UpdateActivePower();
        }
        public double ActivePower=> _panel.ActivePower;
        public void RecalculateSetPoint()
        {
            _panel.UpdateSetPoint();
        }

        public double SetPoint => _panel.SetPoint;
    }
}
