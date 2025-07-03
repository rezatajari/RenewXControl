using RenewXControl.Application.Interfaces.Assets;
using RenewXControl.Domain.Interfaces.Assets;
using Console = System.Console;

namespace RenewXControl.Application.Services.Assets
{
    public class AssetService : IAssetService
    {
        private readonly IBatteryControl _batteryControl;
        private readonly ISolarControl _solarControl;
        private readonly ITurbineControl _turbineControl;

        public AssetService(
            IBatteryControl batteryControl,
            ISolarControl solarControl,
            ITurbineControl turbineControl)
        {
            _batteryControl = batteryControl;
            _solarControl=solarControl;
            _turbineControl = turbineControl;
        }

        private void RecalculateTotalPower()
        {
            _solarControl.UpdateActivePower();
            _turbineControl.UpdateActivePower();

            var totalPower = _solarControl.ActivePower + _turbineControl.ActivePower;
            _batteryControl.RecalculateTotalPower(totalPower);
        }

        public async Task ChargeDischarge()
        {
            switch (_batteryControl.IsNeedToCharge)
            {
                // charging
                case true when _batteryControl.IsStartingChargeDischarge == false:
                    _solarControl.Start();
                    _turbineControl.Start();
                    RecalculateTotalPower();
                    await _batteryControl.Charge();
                    break;

                // when battery need to update new total power for charging
                case true when _batteryControl.IsStartingChargeDischarge == true:
                    RecalculateTotalPower();
                    break;

                // discharging
                case false when _batteryControl.IsStartingChargeDischarge == false:
                    // UpdateSetPointGenerators
                    _solarControl.RecalculateSetPoint();
                    _turbineControl.RecalculateSetPoint();

                    _solarControl.Stop();
                    _turbineControl.Stop();
                    await _batteryControl.Discharge();
                    break;
            }
        }
    }
}
