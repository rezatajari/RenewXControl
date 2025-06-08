namespace RenewXControl.Domain.Assets.Interfaces
{
    public interface ITurbineActive
    {
        void Start();
        void Stop();
        void UpdateWindSpeed();
        void UpdateActivePower();
        double ActivePower { get; }
        double WindSpeed { get; }
    }
}
