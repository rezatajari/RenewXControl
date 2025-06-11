using RenewXControl.Api.Utility;

namespace RenewXControl.Domain.Interfaces.Assets
{
    public interface ICommonEnergyControl
    {
        GeneralResponse<bool> Start();
        GeneralResponse<bool> Stop();
        void UpdateActivePower();
        double ActivePower { get; }
        void RecalculateSetPoint();
        double SetPoint { get; }
    }
}
