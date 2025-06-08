using RenewXControl.Domain.Assets.Interfaces;

namespace RenewXControl.Domain.Assets.Implementatons
{
    public class SolarActive:ISolarActive
    {
        private readonly SolarPanel _panel;
        public SolarActive(SolarPanel panel)
        {
            _panel = panel;
        }

        public void Start()
        {
            _panel.Start();
        }

        public void Stop()
        {
            _panel.Stop();
        }

        public void UpdateIrradiance()
        {
            _panel.UpdateIrradiance();
        }

        public double Irradiance => _panel.Irradiance; 

        public void UpdateActivePower()
        {
           _panel.UpdateActivePower();
        }

        public double ActivePower=> _panel.ActivePower;
    }
}
