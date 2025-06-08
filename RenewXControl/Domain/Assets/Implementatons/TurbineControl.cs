using System.Net;
using RenewXControl.Domain.Assets.Interfaces;

namespace RenewXControl.Domain.Assets.Implementatons
{
    public class TurbineControl:ITurbineControl
    {
        private readonly WindTurbine _turbine;
        public TurbineControl(WindTurbine turbine)
        {
            _turbine = turbine;
        }
        public void Start()
        {
            _turbine.Start();
        }

        public void Stop()
        {
            _turbine.Stop();
        }

        public void UpdateWindSpeed()
        {
            _turbine.UpdateWindSpeed();
        }
        public double WindSpeed => _turbine.WindSpeed;
        public void UpdateSetPoint(double amount)
        {
            _turbine.UpdateSetPoint(amount);
        }

        public void UpdateActivePower()
        {
            _turbine.UpdateActivePower();
        }

        public double ActivePower => _turbine.ActivePower;
    }
}
