using RenewXControl.Api.Utility;

namespace RenewXControl.Domain.Interfaces.Assets;

public interface ITurbineControl
{
    GeneralResponse<bool> Start();
    GeneralResponse<bool> Stop();
    GeneralResponse<bool> UpdateWindSpeed();
    double WindSpeed { get; }
    void UpdateActivePower();
    double ActivePower { get; }
    void RecalculateSetPoint();
    double SetPoint { get; }
}