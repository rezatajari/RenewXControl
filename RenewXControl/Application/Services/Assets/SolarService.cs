using RenewXControl.Api.Utility;
using RenewXControl.Application.Interfaces.Assets;
using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Application.Services.Assets
{
    public class SolarService : ISolarService
    {
        private readonly ISolarControl _solarControl;
        public SolarService(ISolarControl solarActive)
        {
            _solarControl = solarActive;
        }
        public GeneralResponse<bool> StartGenerator()
        {
          return  _solarControl.Start();
        }
        public void TurnOffGenerator()
        {
            _solarControl.Stop();
        }
        public GeneralResponse<bool> UpdateIrradiance()
        {
              return  _solarControl.UpdateIrradiance();
        }
        public double GetIrradiance=> _solarControl.Irradiance;
        public void UpdateActivePower()
        {
            _solarControl.UpdateActivePower();
        }
        public double GetActivePower => _solarControl.ActivePower;
        public void UpdateSetPoint()
        {
            _solarControl.UpdateSetPoint();
        }

        public double GetSetPoint => _solarControl.SetPoint;
    }
}
