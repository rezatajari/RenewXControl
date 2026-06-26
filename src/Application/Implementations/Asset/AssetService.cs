using Application.Common;
using Application.DTOs.AddAsset;
using Application.Interfaces.Asset;
using Domain.Entities.Assets;

namespace Application.Implementations.Asset
{
    public class AssetService(
        IAssetRepository assetRepository,
        ISiteService siteService,
        IUnitOfWork unitOfWork)
        : IAssetService
    {
        public async Task<GeneralResponse<Guid>> AddBattery(Guid userId, AddBattery addBattery, Guid siteId)
        {

            var site = await siteService.HasSite(userId); 
            if (!site.IsSuccess) return GeneralResponse<Guid>.Failure(message:site.Message);

            // Check if the asset can be added
            var canAdd = await siteService.CanAddAsset(siteId, typeof(Battery));
            if (!canAdd.IsSuccess) return GeneralResponse<Guid>.Failure(message: canAdd.Message);

            var battery = Battery.Create(addBattery.Capacity,addBattery.StateCharge,addBattery.SetPoint,addBattery.FrequentlyDischarge, siteId);
            await assetRepository.AddAssetAsync(battery);

            await unitOfWork.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(
                data: battery.Id,
                message: "Battery added successfully");
        }

        public async Task<GeneralResponse<Guid>> AddSolar(Guid userId, AddSolar addSolar, Guid siteId)
        {
            var site = await siteService.HasSite(userId);
            if (!site.IsSuccess) return GeneralResponse<Guid>.Failure(message: site.Message);

            // Check if the asset can be added
            var canAdd = await siteService.CanAddAsset(siteId, typeof(SolarPanel));
            if (!canAdd.IsSuccess) return GeneralResponse<Guid>.Failure(message: canAdd.Message);

            var solar = SolarPanel.Create(addSolar.Irradiance,addSolar.ActivePower,addSolar.SetPoint, siteId);
            await assetRepository.AddAssetAsync(solar);

            await unitOfWork.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(
                solar.Id,
                message: "Solar added successfully");
        }

        public async Task<GeneralResponse<Guid>> AddTurbine(Guid userId, AddTurbine addTurbine, Guid siteId)
        {
            var site = await siteService.HasSite(userId);
            if (!site.IsSuccess) return GeneralResponse<Guid>.Failure(message: site.Message);

            // Check if the asset can be added
            var canAdd = await siteService.CanAddAsset(siteId, typeof(WindTurbine));
            if (!canAdd.IsSuccess) return GeneralResponse<Guid>.Failure(message: canAdd.Message);


            var turbine = WindTurbine.Create(addTurbine.WindSpeed,addTurbine.ActivePower,addTurbine.SetPoint, siteId);
            await assetRepository.AddAssetAsync(turbine);

            await unitOfWork.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(
                turbine.Id,
                message: "Turbine added successfully");
        }
    }
}
