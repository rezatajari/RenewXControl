using RenewXControl.Api.Utility;

namespace RenewXControl.Domain.Interfaces.Assets;

public interface ITurbineControl:ICommonEnergyControl
{
    GeneralResponse<bool> UpdateWindSpeed();
    double WindSpeed { get; }
}