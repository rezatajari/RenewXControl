using RenewXControl.Application.Interfaces;
using RenewXControl.Domain.Assets.Interfaces;

namespace RenewXControl.Application.Services
{
    public class TurbineService : ITurbineService
    {
        private readonly ITurbineControl _windActive;

        public TurbineService(ITurbineControl windActive)
        {
            _windActive = windActive;
        }

        public void StartGenerator()
        {
            _windActive.Start();
        }

        public void TurnOffGenerator()
        {
            _windActive.Stop();
        }

        public void UpdateActivePower()
        {
            _windActive.UpdateActivePower();
        }

        public double GetActivePower => _windActive.ActivePower;
        public void UpdateWindSpeed()
        {
            _windActive.UpdateWindSpeed();
        }

        public double GetWindSpeed => _windActive.WindSpeed;
        public void UpdateSetPoint(double amount)
        {
            _windActive.UpdateSetPoint(amount);
        }
    }
}
