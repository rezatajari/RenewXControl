using RenewXControl.Api.Utility;

namespace RenewXControl.Application.Interfaces.Assets
{
    public interface ISolarService
    {
        GeneralResponse<bool> StartGenerator();
        void TurnOffGenerator();
        void UpdateActivePower();
        double GetActivePower { get; }
        GeneralResponse<bool> UpdateIrradiance();
        double GetIrradiance { get; }
        void UpdateSetPoint(double amount);
    }
}
