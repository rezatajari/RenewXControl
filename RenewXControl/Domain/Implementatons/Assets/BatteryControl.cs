using RenewXControl.Domain.Assets;
using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Domain.Implementatons.Assets
{
    public class BatteryControl:IBatteryControl
    {
        private readonly Battery _battery;
        public BatteryControl(Battery battery)
        {
            _battery = battery;
        }
        public double Capacity => _battery.Capacity;
        public double StateCharge => _battery.StateCharge; 
        public double SetPoint => _battery.SetPoint;
        public double FrequentlyDisCharge => _battery.FrequentlyDisCharge;
        public bool IsNeedToCharge => _battery.IsNeedToCharge;
        public bool IsStartingChargeDischarge => _battery.IsStartingChargeDischarge;
        public string ChargeStateMessage { get; set; }

        public async Task Charge()
        {
            ChargeStateMessage= "Battery is charging";
            await _battery.Charge();
            ChargeStateMessage = "Charge complete.";
        }

        public async Task  Discharge()
        {
            ChargeStateMessage = "Battery is discharging";
            var amountToDischarge = StateCharge - SetPoint;
            var rateOfDischarge = amountToDischarge / FrequentlyDisCharge;
           await   _battery.Discharge(rateOfDischarge);
            ChargeStateMessage = "Discharge complete.";
        }

        public void RecalculateSetPoint()
        {
            _battery.UpdateSetPoint();
        }

        public void RecalculateTotalPower(double amount)
        {
            _battery.SetTotalPower(amount);
        }
    }
}
