using RenewXControl.Application.Interfaces;
using RenewXControl.Domain.Assets.Interfaces;

namespace RenewXControl.Application.Services
{
    public class TurbineService : ITurbineService
    {
        private readonly ITurbineActive _windActive;

        public TurbineService(ITurbineActive windActive)
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

    }
}
