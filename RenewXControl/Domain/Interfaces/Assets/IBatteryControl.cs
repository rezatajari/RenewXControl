namespace RenewXControl.Domain.Interfaces.Assets
{
    public interface IBatteryControl
    {
        double Capacity { get; }
        double StateCharge { get; } 
        double SetPoint { get; }
        double FrequentlyDisCharge { get; }
        bool IsNeedToCharge { get; }
        bool IsStartingCharge { get; }
        Task Charge(double solarAp,double turbineAp);
        Task Discharge();
        void UpdateSetPoint();
    }
}
