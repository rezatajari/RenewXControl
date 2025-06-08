using RenewXControl.Domain.Assets.Interfaces;

namespace RenewXControl.Domain.Assets.Implementatons
{
    public class SolarControl:ISolarControl
    {
        private readonly SolarPanel _panel;
        public SolarControl(SolarPanel panel)
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

        public void UpdateSetPoint(double amount)
        {
            _panel.UpdateSetPoint(amount);
        }

        public double ActivePower=> _panel.ActivePower;
    }
}
