using RenewXControl.Application.Interfaces.Assets;
using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Application.Services.Assets
{
    public class BatteryService:IBatteryService
    {
        private readonly IBatteryControl _batteryControl;

        public BatteryService(IBatteryControl batteryControl)
        {
            _batteryControl = batteryControl;
        }

        public double GetCapacity => _batteryControl.Capacity;
        public double GetStateCharge => _batteryControl.StateCharge;
        public double GetSetPoint => _batteryControl.SetPoint;
        public double GetFrequentlyDisCharge => _batteryControl.FrequentlyDisCharge;
        public bool GetIsNeedToCharge =>_batteryControl.IsNeedToCharge;
        public bool GetIsStartingCharge =>_batteryControl.IsStartingCharge;
        public async Task ChargeAsync(double solarAp, double turbineAp)
        {
           await  _batteryControl.Charge(solarAp, turbineAp);
        }

        public async Task DischargeAsync()
        {
           await _batteryControl.Discharge();
        }

        public void UpdateSetPoint()
        {
            _batteryControl.UpdateSetPoint();
        }
    }
}
