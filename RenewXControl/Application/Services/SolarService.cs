using RenewXControl.Application.Interfaces;
using RenewXControl.Configuration.AssetsModel.Assets;
using RenewXControl.Domain.Assets;
using RenewXControl.Domain.Assets.Interfaces;

namespace RenewXControl.Application.Services
{
    public class SolarService : ISolarService
    {
        private readonly ISolarControl _solarActive;

        public SolarService(ISolarControl solarActive)
        {
            _solarActive = solarActive;
        }

        public void StartGenerator()
        {
            _solarActive.Start();
        }

        public void TurnOffGenerator()
        {
            _solarActive.Stop();
        }

        public void UpdateActivePower()
        {
            _solarActive.UpdateActivePower();
        }

        public double GetActivePower => _solarActive.ActivePower;
        public void UpdateIrradiance()
        {
            _solarActive.UpdateIrradiance();
        }

        public double GetIrradiance=> _solarActive.Irradiance;
        public void UpdateSetPoint(double amount)
        {
            _solarActive.UpdateSetPoint(amount);
        }
    }
}
