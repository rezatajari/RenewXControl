using RenewXControl.Api.Utility;

namespace RenewXControl.Application.Interfaces.Assets
{
    public interface ISolarService
    {
        string StatusMessage { get; set; }
        void StartGenerator();
        void TurnOffGenerator();
        GeneralResponse<bool> UpdateIrradiance();
        double GetIrradiance { get; }
        void UpdateActivePower();
        double GetActivePower { get; }
        void RecalculateSetPoint();
        double GetSetPoint { get; }
    }
}
