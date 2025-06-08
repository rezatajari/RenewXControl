namespace RenewXControl.Application.Interfaces
{
    public interface IBatteryService
    {
        double GetCapacity { get; }
        double GetStateCharge { get; }
        double GetSetPoint { get; }
        double GetFrequentlyDisCharge { get; }
        bool GetIsNeedToCharge { get; }
        bool GetIsStartingCharge { get; }
        Task ChargeAsync(double solarAp, double turbineAp);
        Task DischargeAsync();
        void UpdateSetPoint();
    }
}
