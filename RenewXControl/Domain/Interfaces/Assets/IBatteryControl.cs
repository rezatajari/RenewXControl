namespace RenewXControl.Domain.Interfaces.Assets
{
    public interface IBatteryControl
    {
        double Capacity { get; }
        double StateCharge { get; } 
        double SetPoint { get; }
        double FrequentlyDisCharge { get; }
        bool IsNeedToCharge { get; }
        bool IsStartingChargeDischarge { get; }
        string ChargeStateMessage { get; set; }
        Task Charge();
        Task Discharge();
        void UpdateSetPoint();
        void SetTotalPower(double amount);
    }
}
