using RenewXControl.Api.Utility;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Application.Asset.Interfaces.Asset;
using RenewXControl.Application.DTOs.AddAsset;
using RenewXControl.Domain.Assets;
using RenewXControl.Infrastructure.Persistence;
using Battery = RenewXControl.Domain.Assets.Battery;

namespace RenewXControl.Application.Asset.Implementation.Asset
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly ISiteService _siteService;
        private readonly IUserValidator _userValidator;
        private readonly RxcDbContext _context;

        public AssetService(
            IAssetRepository assetRepository,
            ISiteService siteService,
            RxcDbContext context,
            IUserValidator userValidator)
        {
            _assetRepository = assetRepository;
            _siteService = siteService;
            _context = context;
            _userValidator = userValidator;
        }


        public async Task<GeneralResponse<Guid>> AddBatteryAsync(AddBattery addBattery, string userId)
        {
            var userValidation = _userValidator.ValidateUserId(userId);
            if (!userValidation.IsSuccess)
                return GeneralResponse<Guid>.Failure(message: userValidation.Message, errors: userValidation.Errors);

            var siteIdResponse = await _siteService.GetSiteId(userId);
            var siteId = siteIdResponse.Data;

            var battery = Battery.Create(addBattery, siteId);
            await _assetRepository.AddAssetAsync(battery);

            await _context.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(
                data: battery.Id,
                message: "Battery added successfully");
        }

        public async Task<GeneralResponse<Guid>> AddSolarAsync(AddSolar addSolar, string userId)
        {
            var userValidation = _userValidator.ValidateUserId(userId);
            if (!userValidation.IsSuccess)
                return GeneralResponse<Guid>.Failure(message: userValidation.Message, errors: userValidation.Errors);

            var siteIdResponse = await _siteService.GetSiteId(userId);
            var siteId = siteIdResponse.Data;

            var solar = SolarPanel.Create(addSolar, siteId);
            await _assetRepository.AddAssetAsync(solar);

            await _context.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(
                solar.Id,
                message: "Solar added successfully");
        }

        public async Task<GeneralResponse<Guid>> AddTurbineAsync(AddTurbine addTurbine, string userId)
        {
            var userValidation = _userValidator.ValidateUserId(userId);
            if (!userValidation.IsSuccess)
                return GeneralResponse<Guid>.Failure(message: userValidation.Message, errors: userValidation.Errors);

            var siteIdResponse = await _siteService.GetSiteId(userId);
            var siteId = siteIdResponse.Data;

            var turbine = WindTurbine.Create(addTurbine, siteId);
            await _assetRepository.AddAssetAsync(turbine);

            await _context.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(
                turbine.Id,
                message: "Turbine added successfully");
        }

        public async Task<GeneralResponse<SolarPanel>> GetSolarByUserIdAsync(string userId)
        {
            var userValidation = _userValidator.ValidateUserId(userId);
            if (!userValidation.IsSuccess)
                return GeneralResponse<SolarPanel>.Failure(message: userValidation.Message, errors: userValidation.Errors);

            var solar = await _assetRepository.GetSolarByUserId(userId);

            return GeneralResponse<SolarPanel>.Success(
                data:solar,
                message:"Solar found is successfully"
                );
        }

        public async Task<GeneralResponse<WindTurbine>> GetTurbineByUserIdAsync(string userId)
        {
            var userValidation = _userValidator.ValidateUserId(userId);
            if (!userValidation.IsSuccess)
                return GeneralResponse<WindTurbine>.Failure(message: userValidation.Message, errors: userValidation.Errors);

            var turbine = await _assetRepository.GetTurbineByUserId(userId);

            return GeneralResponse<WindTurbine>.Success(
                data: turbine,
                message: "Solar found is successfully"
            );
        }

        public async Task<GeneralResponse<Battery>> GetBatteryByUserIdAsync(string userId)
        {
            var userValidation = _userValidator.ValidateUserId(userId);
            if (!userValidation.IsSuccess)
                return GeneralResponse<Battery>.Failure(message: userValidation.Message, errors: userValidation.Errors);

            var battery = await _assetRepository.GetBatteryByUserId(userId);

            return GeneralResponse<Battery>.Success(
                data: battery,
                message: "Solar found is successfully"
            );
        }
    }
}
