using RenewXControl.Api.Utility;

namespace RenewXControl.Domain.Interfaces.Assets
{
    public interface ISolarControl
    {
        GeneralResponse<bool> Start();
        void Stop();
        GeneralResponse<bool> UpdateIrradiance();
        double Irradiance { get; }
        void UpdateActivePower();
        double ActivePower { get; }
        void UpdateSetPoint();
        double SetPoint { get; }
    }
}
