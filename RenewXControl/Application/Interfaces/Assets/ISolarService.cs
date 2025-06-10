using RenewXControl.Api.Utility;

namespace RenewXControl.Application.Interfaces.Assets
{
    public interface ISolarService
    {
        GeneralResponse<bool> StartGenerator();
        void TurnOffGenerator();
        GeneralResponse<bool> UpdateIrradiance();
        double GetIrradiance { get; }
        void UpdateActivePower();
        double GetActivePower { get; }
        void UpdateSetPoint();
        double GetSetPoint { get; }
    }
}
