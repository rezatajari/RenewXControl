namespace RenewXControl.Domain.Assets.Interfaces
{
    public interface ITurbineControl
    {
        void Start();
        void Stop();
        void UpdateWindSpeed();
        void UpdateActivePower();
        double ActivePower { get; }
        double WindSpeed { get; }
        void UpdateSetPoint(double amount);
    }
}
