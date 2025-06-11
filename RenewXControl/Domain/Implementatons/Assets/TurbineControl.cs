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
        public GeneralResponse<bool> Stop()
        {
            _turbine.Stop();
            return GeneralResponse<bool>.Success(data:true,message: "Wind turbine is off",isSuccess:true);
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
