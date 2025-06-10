using RenewXControl.Api.Utility;

namespace RenewXControl.Domain.Interfaces.Assets
{
    public interface ISolarControl
    {
        GeneralResponse<bool> Start();
        void Stop();
        GeneralResponse<bool> UpdateIrradiance();
        double Irradiance { get; }
        double ActivePower { get; }
        void UpdateActivePower();
        void UpdateSetPoint(double amount);
    }
}
