using RenewXControl.Api.Utility;

namespace RenewXControl.Application.Interfaces.Assets
{
    public interface ITurbineService
    {
        GeneralResponse<bool> StartGenerator();
        void TurnOffGenerator();
        GeneralResponse<bool> UpdateWindSpeed();
        double GetWindSpeed { get; }
        void UpdateActivePower();
        double GetActivePower { get; }
        void UpdateSetPoint();
        double GetSetPoint { get; }
    }
}
