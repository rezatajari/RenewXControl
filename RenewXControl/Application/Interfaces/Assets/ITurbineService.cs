using RenewXControl.Api.Utility;

namespace RenewXControl.Application.Interfaces.Assets
{
    public interface ITurbineService
    {
        GeneralResponse<bool> StartGenerator();
        void TurnOffGenerator();
        void UpdateActivePower();
        double GetActivePower { get; }
        GeneralResponse<bool> UpdateWindSpeed();
        double GetWindSpeed { get; }
        void UpdateSetPoint(double amount);
    }
}
