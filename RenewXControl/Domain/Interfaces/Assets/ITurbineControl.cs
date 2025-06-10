using RenewXControl.Api.Utility;

namespace RenewXControl.Domain.Interfaces.Assets
{
    public interface ITurbineControl
    {
        GeneralResponse<bool> Start();
        void Stop();
        GeneralResponse<bool> UpdateWindSpeed();
        void UpdateActivePower();
        double ActivePower { get; }
        double WindSpeed { get; }
        void UpdateSetPoint(double amount);
    }
}
