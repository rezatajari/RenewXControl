using RenewXControl.Api.Utility;
using RenewXControl.Application.Interfaces.Assets;
using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Application.Services.Assets
{
    public class TurbineService : ITurbineService
    {
        private readonly ITurbineControl _turbineControl;
        public TurbineService(ITurbineControl windActive)
        {
            _turbineControl = windActive;
        }

        public string StatusMessage { get; set; }

        public void StartGenerator()
        {
         var result=    _turbineControl.Start();
         StatusMessage = result.Message;
        }
        public void TurnOffGenerator()
        {
            var result =_turbineControl.Stop();
            StatusMessage = result.Message;

        }
        public GeneralResponse<bool> UpdateWindSpeed()
        {
            return _turbineControl.UpdateWindSpeed();
        }
        public double GetWindSpeed => _turbineControl.WindSpeed;
        public void UpdateActivePower()
        {
            _turbineControl.UpdateActivePower();
        }
        public double GetActivePower => _turbineControl.ActivePower;
        public void UpdateSetPoint()
        {
            _turbineControl.UpdateSetPoint();
        }
        public double GetSetPoint => _turbineControl.SetPointSpeed;
    }
}
