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
        public bool IsStartingCharge => _battery.IsStartingCharge;
        public string ChargeStateMessage { get; set; }

        public async Task Charge(double solarAp,double turbineAp)
        {
            ChargeStateMessage= "Battery is charging";
            var totalPower = solarAp + turbineAp;
            await _battery.Charge(totalPower);
            ChargeStateMessage = "Charge complete.";
        }

        public async Task Discharge()
        {
            ChargeStateMessage = "Battery is discharging";
            var amountToDischarge = StateCharge - SetPoint;
            var rateOfDischarge = amountToDischarge / FrequentlyDisCharge;
            await  _battery.Discharge(rateOfDischarge);
            ChargeStateMessage = "Discharge complete.";
        }

        public void UpdateSetPoint()
        {
            _battery.UpdateSetPoint();
        }
    }
}
