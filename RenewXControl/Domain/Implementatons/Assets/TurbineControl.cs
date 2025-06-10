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
        public GeneralResponse<bool> Start()
        {
            if (_turbine.Start())
            {
                return GeneralResponse<bool>.Success(data: true, message: "Wind turbine is generating power", isSuccess: true);
            }
            else
            {
                return GeneralResponse<bool>.Failure(message: "Wind turbine is not generating power", isSuccess: false);
            }
        }

        public void Stop()
        {
            _turbine.Stop();
        }

        public GeneralResponse<bool> UpdateWindSpeed()
        {
            if (_turbine.UpdateWindSpeed())
            {
                return GeneralResponse<bool>.Success(data: true, message: "Wind turbine is generating power", isSuccess: true);
            }
            else
            {
                return GeneralResponse<bool>.Failure(message: "Wind turbine is not generating power", isSuccess: false);
            }
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
