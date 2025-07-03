using System.Runtime.InteropServices;
using RenewXControl.Api.Utility;
using RenewXControl.Application.DTOs.Asset;
using RenewXControl.Application.Interfaces;
using RenewXControl.Domain.Assets;
using RenewXControl.Domain.Interfaces;
using RenewXControl.Domain.Interfaces.Assets;
using RenewXControl.Infrastructure.Persistence.MyDbContext;

namespace RenewXControl.Application.Services
{
    public class AssetSettingService : IAssetSettingService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly RxcDbContext _context;
        public AssetSettingService(IAssetRepository assetRepository,RxcDbContext context)
        {
            _assetRepository = assetRepository;
            _context = context;
        }

        public async Task<GeneralResponse<Guid>> AddBatteryAsync(AddBattery addBattery, string userId)
        {
            var battery = Battery.Create(addBattery);
            await _assetRepository.AddAsync(battery);

            await _context.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(data: battery.Id, "Battery added successfully");



        }

        public async Task<GeneralResponse<Guid>> AddBatteryAsync(AddSolar addSolar, string userId)
        {
            var solar = SolarPanel.Create(addSolar);
            await _assetRepository.AddAsync(solar);

            await _context.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(solar.Id, "Solar added successfully");
        }

        public async Task<GeneralResponse<Guid>> AddBatteryAsync(AddTurbine addTurbine, string userId)
        {
            var turbine = WindTurbine.Create(addTurbine);
            await _assetRepository.AddAsync(turbine);

            await _context.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(turbine.Id, "Turbine added successfully");
        }
    }
}
