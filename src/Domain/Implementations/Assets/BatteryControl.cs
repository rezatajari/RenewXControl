using Domain.Entities.Assets;
using Domain.Interfaces.Assets;

namespace Domain.Implementations.Assets;

public class BatteryControl(Battery battery) : IBatteryControl
{
    public double Capacity => battery.Capacity;
    public double StateCharge => battery.StateCharge; 
    public double SetPoint => battery.SetPoint;
    public double FrequentlyDisCharge => battery.FrequentlyDisCharge;
    public bool IsNeedToCharge => battery.IsNeedToCharge;
    public bool IsStartingChargeDischarge => battery.IsStartingChargeDischarge;
    public string ChargeStateMessage { get; set; }=string.Empty;

    public async Task Charge()
    {
        ChargeStateMessage= "Battery is charging";
        await battery.Charge();
        ChargeStateMessage = "Charge complete.";
        battery.UpdateSetPoint();
    }

    public async Task  Discharge()
    {
        ChargeStateMessage = "Battery is discharging";
        var amountToDischarge = StateCharge - SetPoint;
        var rateOfDischarge = amountToDischarge / FrequentlyDisCharge;
        await   battery.Discharge(rateOfDischarge);
        ChargeStateMessage = "Discharge complete.";
    }

    public void RecalculateTotalPower(double amount)
    {
        battery.SetTotalPower(amount);
    }
}