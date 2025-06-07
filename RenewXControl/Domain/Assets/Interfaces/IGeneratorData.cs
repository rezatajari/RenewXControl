namespace RenewXControl.Domain.Assets.Interfaces
{
    public interface IGeneratorData
    {
        void Start();
        void Stop();
        void UpdateSensor();
        double GetSensor();
        void UpdateActivePower();
        double GetActivePower();
    }
}
