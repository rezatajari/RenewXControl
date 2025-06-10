namespace RenewXControl.Application.Interfaces.Assets
{
    public interface IBatteryService
    {
        double GetCapacity { get; }
        double GetStateCharge { get; }
        double GetSetPoint { get; }
        double GetFrequentlyDisCharge { get; }
        bool GetIsNeedToCharge { get; }
        bool GetIsStartingChargeDischarge { get; }
        string GetChargeStateMessage { get;}
        Task ChargeAsync();
        Task DischargeAsync();
        void SetTotalPower(double amount);
    }
}
