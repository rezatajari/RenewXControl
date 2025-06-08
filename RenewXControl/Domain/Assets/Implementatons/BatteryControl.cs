using RenewXControl.Domain.Assets.Interfaces;

namespace RenewXControl.Domain.Assets.Implementatons
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
        public async Task Charge(double solarAp,double turbineAp)
        {
          await _battery.Charge(solarAp, turbineAp);
        }

        public async Task Discharge()
        {
           await  _battery.Discharge();
        }

        public void UpdateSetPoint()
        {
            _battery.UpdateSetPoint();
        }
    }
}
