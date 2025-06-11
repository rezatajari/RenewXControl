using RenewXControl.Api.Utility;

namespace RenewXControl.Application.Interfaces.Assets
{
    public interface ITurbineService
    {
        string StatusMessage { get; set; }
        void StartGenerator();
        void TurnOffGenerator();
        GeneralResponse<bool> UpdateWindSpeed();
        double GetWindSpeed { get; }
        void UpdateActivePower();
        double GetActivePower { get; }
        void RecalculateSetPoint();
        double GetSetPoint { get; }
    }
}
