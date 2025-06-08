namespace RenewXControl.Domain.Assets.Interfaces
{
    public interface ISolarControl
    {
        void Start();
        void Stop();
        void UpdateIrradiance();
        double Irradiance { get; }
        double ActivePower { get; }
        void UpdateActivePower();
        void UpdateSetPoint(double amount);
    }
}
