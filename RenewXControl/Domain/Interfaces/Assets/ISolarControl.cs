using RenewXControl.Api.Utility;

namespace RenewXControl.Domain.Interfaces.Assets;

public interface ISolarControl
{
    GeneralResponse<bool> Start();
    GeneralResponse<bool> Stop();
    GeneralResponse<bool> UpdateIrradiance();
    double Irradiance { get; }
    void UpdateActivePower();
    double ActivePower { get; }
    void RecalculateSetPoint();
    double SetPoint { get; }
}