using RenewXControl.Domain.Assets.Interfaces;

namespace RenewXControl.Application.Interfaces
{
    public interface ITurbineService
    {
        void StartGenerator();
        void TurnOffGenerator();
        void UpdateActivePower();
        double GetActivePower();
        void UpdateWindSpeed();
        double GetWindSpeed();
    }
}
