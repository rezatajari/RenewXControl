using Application.Common;
using Application.DTOs.AddAsset;
using Application.Interfaces.Asset;
using Application.Interfaces.User;
using Domain.Entities.Site;

namespace Application.Implementations.Asset;

public class SiteService:ISiteService
{
    private readonly ISiteRepository _siteRepository;
    private readonly IUserValidator _userValidator;
    private readonly IUnitOfWork _unitOfWork;

    public SiteService(ISiteRepository siteRepository, IUnitOfWork unitOfWork, IUserValidator userValidator)
    {
        _siteRepository = siteRepository;
        _unitOfWork = unitOfWork;
        _userValidator = userValidator;
    }
    public async Task<GeneralResponse<Guid>> AddSiteAsync(AddSite addSite, Guid userId)
    {
        var userValidation = _userValidator.ValidateUserId(userId);
        if (!userValidation.IsSuccess)
            return GeneralResponse<Guid>.Failure(message:userValidation.Message,errors:userValidation.Errors);

        var site = Site.Create(addSite.Name,addSite.Location, userId);
        await _siteRepository.AddAsync(site);

        await _unitOfWork.SaveChangesAsync();

        return GeneralResponse<Guid>.Success(
            data: site.Id,
            message:"Site added successfully"
        );
    }

    public async Task<GeneralResponse<Guid>> GetSiteId(Guid userId)
    {
        var userValidation = _userValidator.ValidateUserId(userId);
        if (!userValidation.IsSuccess)
            return GeneralResponse<Guid>.Failure(message: userValidation.Message, errors: userValidation.Errors);

        var siteId = await _siteRepository.GetIdAsync(userId);
        if (siteId == Guid.Empty)
        {
            return GeneralResponse<Guid>.Failure(
                message:"Site Unfounded",
                errors: [
                    new ErrorResponse
                    {
                        Name = "Site",
                        Message = "The user does not own any site"
                    }
                ]);
        }

        return GeneralResponse<Guid>.Success(siteId);
    }

    public async Task<GeneralResponse<bool>> HasSiteAsync(Guid userId)
    {
        var response =await _siteRepository.HasSite(userId);
        if (response)
            return GeneralResponse<bool>.Success(data: true, message: "Has site operation is successful");

        return GeneralResponse<bool>.Failure(
            message:"Site NotFound",
            errors: [
                new ErrorResponse
                {
                    Name = "HasSite",
                    Message = "Your site is not created until now"
                }
            ]);
    }
}