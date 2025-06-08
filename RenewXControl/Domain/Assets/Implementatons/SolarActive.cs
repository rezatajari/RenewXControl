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

        public double GetIrradiance()
        {
            return _panel.GetIrradiance();
        }

        public void UpdateActivePower()
        {
           _panel.UpdateActivePower();
        }

        public double GetActivePower()
        {
          return  _panel.GetActivePower();
        }
    }
}
