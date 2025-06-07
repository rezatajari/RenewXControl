namespace RenewXControl.Domain.Assets.Interfaces
{
    public interface IGeneratorData
    {
        void UpdateSensor();
        double GetSensor();
        void UpdateActivePower();
        double GetActivePower();
    }
}
