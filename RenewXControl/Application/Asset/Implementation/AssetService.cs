using RenewXControl.Api.Utility;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Application.DTOs.AddAsset;
using RenewXControl.Domain.Assets;
using RenewXControl.Domain.Interfaces.Assets;
using RenewXControl.Infrastructure.Persistence;
using RenewXControl.Infrastructure.Services.Asset;
using Console = System.Console;

namespace RenewXControl.Application.Asset.Implementation
{
    public class AssetService : IAssetService
    {
        private readonly IBatteryControl _batteryControl;
        private readonly ISolarControl _solarControl;
        private readonly ITurbineControl _turbineControl;
        private readonly IAssetRepository _assetRepository;
        private readonly RxcDbContext _context;

        public AssetService(
            IBatteryControl batteryControl,
        ISolarControl solarControl,
            ITurbineControl turbineControl,
            IAssetRepository assetRepository, 
            RxcDbContext context)
        {
            _batteryControl = batteryControl;
            _solarControl=solarControl;
            _turbineControl = turbineControl;
            _assetRepository = assetRepository;
            _context = context;
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


        public async Task<GeneralResponse<Guid>> AddBatteryAsync(AddBattery addBattery, string userId)
        {
            var battery = Battery.Create(addBattery);
            await _assetRepository.AddAsync(battery);

            await _context.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(data: battery.Id, "Battery added successfully");
        }

        public async Task<GeneralResponse<Guid>> AddSolarAsync(AddSolar addSolar, string userId)
        {
            var solar = SolarPanel.Create(addSolar);
            await _assetRepository.AddAsync(solar);

            await _context.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(solar.Id, "Solar added successfully");
        }

        public async Task<GeneralResponse<Guid>> AddTurbineAsync(AddTurbine addTurbine, string userId)
        {
            var turbine = WindTurbine.Create(addTurbine);
            await _assetRepository.AddAsync(turbine);

            await _context.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(turbine.Id, "Turbine added successfully");
        }
    }
}
