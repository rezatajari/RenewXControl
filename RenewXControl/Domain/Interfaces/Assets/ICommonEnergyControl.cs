using RenewXControl.Api.Utility;

namespace RenewXControl.Domain.Interfaces.Assets
{
    public interface ICommonEnergyControl
    {
        string StatusMessage { get; set; }
        bool Start();
        bool Stop();
        void UpdateActivePower();
        double ActivePower { get; }
        void RecalculateSetPoint();
        double SetPoint { get; }
    }
}
