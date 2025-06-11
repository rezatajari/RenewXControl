using RenewXControl.Application.Interfaces.Assets;
using Console = System.Console;

namespace RenewXControl.Application.Services.Assets
{
    public class AssetService : IAssetService
    {
        private readonly IBatteryService _batteryService;
        private readonly ISolarService _solarService;
        private readonly ITurbineService _turbineService;

        public AssetService(
            IBatteryService batteryService,
            ISolarService solarService,
            ITurbineService turbineService)
        {
            _batteryService = batteryService;
            _solarService = solarService;
            _turbineService = turbineService;
        }

        private void UpdateTotalPower()
        {
            UpdateGenerators();

            var totalPower = _solarService.GetActivePower + _turbineService.GetActivePower;
            _batteryService.SetTotalPower(totalPower);
        }
        private void UpdateGenerators()
        {
            _solarService.UpdateActivePower();
            _turbineService.UpdateActivePower();
        }
        private void UpdateSetPointGenerators()
        {
            _solarService.RecalculateSetPoint();
            _turbineService.RecalculateSetPoint();
        }


        public void StartGenerators()
        {
            _solarService.StartGenerator();
            _turbineService.StartGenerator();
        }
        public async Task ChargeDischarge()
        {
            switch (_batteryService.GetIsNeedToCharge)
            {
                // charging
                case true when _batteryService.GetIsStartingChargeDischarge == false:
                    StartGenerators();
                    UpdateTotalPower();
                    await _batteryService.ChargeAsync();
                    break;

                // when battery need to update new total power for charging
                case true when _batteryService.GetIsStartingChargeDischarge == true:
                    UpdateTotalPower();
                    break;

                // discharging
                case false when _batteryService.GetIsStartingChargeDischarge == false:
                     TurnOffGenerators();
                  await  _batteryService.DischargeAsync();
                    break;
            }
        }
        public void TurnOffGenerators()
        {
            UpdateSetPointGenerators();
            _solarService.TurnOffGenerator();
            _turbineService.TurnOffGenerator();
        }
    }
}
