namespace RenewXControl.Domain.Assets.Interfaces
{
    public interface ISolarActive
    {
        void Start();
        void Stop();
        void UpdateIrradiance();
        double Irradiance { get; }
        double ActivePower { get; }
        void UpdateActivePower();
    }
}
