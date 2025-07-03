using RenewXControl.Api.Utility;

namespace RenewXControl.Domain.Interfaces.Assets;

public interface ISolarControl:ICommonEnergyControl
{
    bool UpdateIrradiance();
    double Irradiance { get; }
}