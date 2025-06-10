using RenewXControl.Application.Interfaces.Assets;

namespace RenewXControl.Application.Services.Assets
{
    public class AssetControl : IAssetControl
    {
        private readonly IBatteryService _batteryService;
        private readonly ISolarService _solarService;
        private readonly ITurbineService _turbineService;

        public AssetControl(
            IBatteryService batteryService,
            ISolarService solarService,
            ITurbineService turbineService)
        {
            _batteryService = batteryService;
            _solarService = solarService;
            _turbineService = turbineService;
        }

        public async Task ChargeDischarge()
        {
            switch (_batteryService.GetIsNeedToCharge)
            {
                // charging
                case true when _batteryService.GetIsStartingCharge == false:
                    _solarService.UpdateSetPoint();
                    _turbineService.UpdateSetPoint();
                    _ = _batteryService.ChargeAsync(
                                _solarService.GetActivePower,
                              _turbineService.GetActivePower);
                    break;

                // discharging
                case false when _batteryService.GetIsStartingCharge == false:
                    _batteryService.UpdateSetPoint();
                    _solarService.TurnOffGenerator();
                    _turbineService.TurnOffGenerator();
                    _ = _batteryService.DischargeAsync();
                    break;
            }
        }
    }
}
