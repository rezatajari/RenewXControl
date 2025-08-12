using Application.Common;
using Application.DTOs.AddAsset;
using Application.Interfaces.Asset;
using Application.Interfaces.User;
using Domain.Entities.Assets;

namespace Application.Implementations.Asset
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly ISiteService _siteService;
        private readonly IUsersService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public AssetService(
            IAssetRepository assetRepository,
            ISiteService siteService,
            IUnitOfWork unitOfWork,
            IUsersService userService)
        {
            _assetRepository = assetRepository;
            _siteService = siteService;
            _userService = userService;
            _unitOfWork= unitOfWork;
        }


        public async Task<GeneralResponse<Guid>> AddBatteryAsync(AddBattery addBattery, Guid userId)
        {
            var userValidation = _userService.ValidateUserId(userId);
            if (!userValidation.IsSuccess)
                return GeneralResponse<Guid>.Failure(message: userValidation.Message, errors: userValidation.Errors);

            var siteIdResponse = await _siteService.GetSiteId(userId);
            if (!siteIdResponse.IsSuccess)
                return GeneralResponse<Guid>.Failure(
                    message:siteIdResponse.Message,
                    errors:siteIdResponse.Errors
                    );

            var siteId = siteIdResponse.Data;

            var battery = Battery.Create(addBattery.Capacity,addBattery.StateCharge,addBattery.SetPoint,addBattery.FrequentlyDischarge, siteId);
            await _assetRepository.AddAssetAsync(battery);

            await _unitOfWork.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(
                data: battery.Id,
                message: "Battery added successfully");
        }

        public async Task<GeneralResponse<Guid>> AddSolarAsync(AddSolar addSolar, Guid userId)
        {
            var userValidation = _userService.ValidateUserId(userId);
            if (!userValidation.IsSuccess)
                return GeneralResponse<Guid>.Failure(message: userValidation.Message, errors: userValidation.Errors);

            var siteIdResponse = await _siteService.GetSiteId(userId);
            if (!siteIdResponse.IsSuccess)
                return GeneralResponse<Guid>.Failure(
                    message: siteIdResponse.Message,
                    errors: siteIdResponse.Errors
                );

            var siteId = siteIdResponse.Data;

            var solar = SolarPanel.Create(addSolar.Irradiance,addSolar.ActivePower,addSolar.SetPoint, siteId);
            await _assetRepository.AddAssetAsync(solar);

            await _unitOfWork.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(
                solar.Id,
                message: "Solar added successfully");
        }

        public async Task<GeneralResponse<Guid>> AddTurbineAsync(AddTurbine addTurbine, Guid userId)
        {
            var userValidation = _userService.ValidateUserId(userId);
            if (!userValidation.IsSuccess)
                return GeneralResponse<Guid>.Failure(message: userValidation.Message, errors: userValidation.Errors);

            var siteIdResponse = await _siteService.GetSiteId(userId);
            if (!siteIdResponse.IsSuccess)
                return GeneralResponse<Guid>.Failure(
                    message: siteIdResponse.Message,
                    errors: siteIdResponse.Errors
                );

            var siteId = siteIdResponse.Data;

            var turbine = WindTurbine.Create(addTurbine.WindSpeed,addTurbine.ActivePower,addTurbine.SetPoint, siteId);
            await _assetRepository.AddAssetAsync(turbine);

            await _unitOfWork.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(
                turbine.Id,
                message: "Turbine added successfully");
        }

        public async Task<GeneralResponse<SolarPanel>> GetSolarByUserIdAsync(Guid userId)
        {
            var userValidation = _userService.ValidateUserId(userId);
            if (!userValidation.IsSuccess)
                return GeneralResponse<SolarPanel>.Failure(message: userValidation.Message, errors: userValidation.Errors);

            var solar = await _assetRepository.GetSolarByUserId(userId);

            return GeneralResponse<SolarPanel>.Success(
                data:solar,
                message:"Solar found is successfully"
                );
        }

        public async Task<GeneralResponse<WindTurbine>> GetTurbineByUserIdAsync(Guid userId)
        {
            var userValidation = _userService.ValidateUserId(userId);
            if (!userValidation.IsSuccess)
                return GeneralResponse<WindTurbine>.Failure(message: userValidation.Message, errors: userValidation.Errors);

            var turbine = await _assetRepository.GetTurbineByUserId(userId);

            return GeneralResponse<WindTurbine>.Success(
                data: turbine,
                message: "Solar found is successfully"
            );
        }

        public async Task<GeneralResponse<Battery>> GetBatteryByUserIdAsync(Guid userId)
        {
            var userValidation = _userService.ValidateUserId(userId);
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
