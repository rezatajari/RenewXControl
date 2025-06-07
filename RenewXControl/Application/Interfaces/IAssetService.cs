namespace RenewXControl.Application.Interfaces
{
    public interface IAssetService
    {
        void StartGenerator();
        void TurnOffGenerator();
        void UpdateSetPoint(double amount);
        void UpdateSensor();
        double GetSensor();
        void UpdateActivePower();
        double GetActivePower();
    }
}
