using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Domain.Interfaces.Assets;
using RenewXControl.Infrastructure.Persistence;

namespace RenewXControl.Application.Asset.Implementation
{
    public class AssetRuntimeOperation:IAssetRuntimeOperation
    {
        private readonly IBatteryControl _batteryControl;
        private readonly ISolarControl _solarControl;
        private readonly ITurbineControl _turbineControl;

        public AssetRuntimeOperation(
            IBatteryControl batteryControl,
            ISolarControl solarControl,
            ITurbineControl turbineControl)
        {
            _batteryControl = batteryControl;
            _solarControl = solarControl;
            _turbineControl = turbineControl;
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

        private void RecalculateTotalPower()
        {
            _solarControl.UpdateActivePower();
            _turbineControl.UpdateActivePower();

            var totalPower = _solarControl.ActivePower + _turbineControl.ActivePower;
            _batteryControl.RecalculateTotalPower(totalPower);
        }
    }
}
