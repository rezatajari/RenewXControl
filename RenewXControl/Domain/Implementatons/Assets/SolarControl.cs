using RenewXControl.Api.Utility;
using RenewXControl.Domain.Assets;
using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Domain.Implementatons.Assets
{
    public class SolarControl:ISolarControl
    {
        private readonly SolarPanel _panel;
        public SolarControl(SolarPanel panel)
        {
            _panel = panel;
        }
        public GeneralResponse<bool> Start()
        {
            if (_panel.Start())
            {
                 return GeneralResponse<bool>.Success(data:true,message: "Solar panel is generating power", isSuccess: true);
            }
            else
            {
                return GeneralResponse<bool>.Failure(message: "Solar panel is not generating power", isSuccess: false);
            }
        }
        public void Stop()
        {
            _panel.Stop();
        }
        public GeneralResponse<bool> UpdateIrradiance()
        {
            if (_panel.UpdateIrradiance())
            {
                return GeneralResponse<bool>.Success(data: true, message: "Solar panel is generating power", isSuccess: true);
            }
            else
            {
                return GeneralResponse<bool>.Failure(message: "Solar panel is not generating power", isSuccess: false);
            }
        }
        public double Irradiance => _panel.Irradiance; 
        public void UpdateActivePower()
        {
           _panel.UpdateActivePower();
        }
        public double ActivePower=> _panel.ActivePower;
        public void UpdateSetPoint()
        {
            _panel.UpdateSetPoint();
        }

        public double SetPoint => _panel.SetPoint;
    }
}
