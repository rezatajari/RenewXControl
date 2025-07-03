using System.Net;
using RenewXControl.Api.Utility;
using RenewXControl.Domain.Assets;
using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Domain.Implementatons.Assets
{
    public class TurbineControl:ITurbineControl
    {
        private readonly WindTurbine _turbine;
        public TurbineControl(WindTurbine turbine)
        {
            _turbine = turbine;
        }

        public string StatusMessage { get; set; }

        public bool Start()
        {
            if (_turbine.Start())
            {
                StatusMessage = "Wind turbine is generating power";
                return true;
            }
            else
            {
                StatusMessage = "Wind turbine is not generating power";
                return false;
            }
        }
        public bool Stop()
        {
            _turbine.Stop();
            StatusMessage = "Wind turbine is off";
            return true;
        }
        public bool UpdateWindSpeed()
        {
            if (_turbine.UpdateWindSpeed())
            {
                StatusMessage = "Wind turbine is generating power";
                return true;
            }
            else
            {
                StatusMessage = "Wind turbine is not generating power";
                return false;
            }
        }
        public double WindSpeed => _turbine.WindSpeed;
        public void UpdateActivePower()
        {
            _turbine.UpdateActivePower();
        }
        public double ActivePower => _turbine.ActivePower;
        public void RecalculateSetPoint()
        {
            _turbine.UpdateSetPoint();
        }

        public double SetPoint => _turbine.SetPoint;
    }
}
