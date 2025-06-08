using RenewXControl.Application.Interfaces;
using RenewXControl.Configuration.AssetsModel.Assets;
using RenewXControl.Domain.Assets;
using RenewXControl.Domain.Assets.Interfaces;

namespace RenewXControl.Application.Services
{
    public class SolarService : ISolarService
    {
        private readonly ISolarActive _solarActive;
        public SolarService(ISolarActive solarActive)
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

        public double GetActivePower()
        {
          return  _solarActive.GetActivePower();
        }

        public void UpdateIrradiance()
        {
            _solarActive.UpdateIrradiance();
        }

        public double GetIrradiance()
        {
            return _solarActive.GetIrradiance();
        }
    }
}
