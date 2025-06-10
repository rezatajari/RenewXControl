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
        public bool GetIsStartingChargeDischarge =>_batteryControl.IsStartingChargeDischarge;
        public string GetChargeStateMessage => _batteryControl.ChargeStateMessage;

        public async Task ChargeAsync()
        {
           await  _batteryControl.Charge();
           UpdateSetPoint();
        }

        public async Task DischargeAsync()
        {
          await _batteryControl.Discharge();
        }

        public void SetTotalPower(double amount)
        {
            _batteryControl.SetTotalPower(amount);
        }

        private void UpdateSetPoint()
        {
            _batteryControl.UpdateSetPoint();
        }
    }
}
