namespace RenewXControl.Domain.Assets.Interfaces
{
    public interface ISolarActive
    {
        void Start();
        void Stop();
        void UpdateIrradiance();
        double GetIrradiance();
        void UpdateActivePower();
        double GetActivePower();
    }
}
