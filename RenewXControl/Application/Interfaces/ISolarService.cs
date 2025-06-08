using RenewXControl.Domain.Assets.Interfaces;

namespace RenewXControl.Application.Interfaces
{
    public interface ISolarService
    {
        void StartGenerator();
        void TurnOffGenerator();
        void UpdateActivePower();
        double GetActivePower { get; }
        void UpdateIrradiance();
        double GetIrradiance { get; }
    }
}
