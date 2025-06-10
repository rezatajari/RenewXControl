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

        public string StatusMessage { get; set; }

        public void StartGenerator()
        {
           var result= _solarControl.Start();
           StatusMessage = result.Message;
        }
        public void TurnOffGenerator()
        {
         var result=   _solarControl.Stop();
         StatusMessage = result.Message;
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
