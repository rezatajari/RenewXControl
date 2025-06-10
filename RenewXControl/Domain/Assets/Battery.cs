using Microsoft.AspNetCore.Http.Metadata;
using RenewXControl.Configuration.AssetsModel.Assets;
using RenewXControl.Domain.Interfaces.Assets;


namespace RenewXControl.Domain.Assets
{
    public class Battery : Asset
    {
        private Battery() { }
        private Battery(double capacity, double stateCharge, double setPoint, double frequentlyDisCharge)
        {
            FrequentlyDisCharge = frequentlyDisCharge;
            Name = $"Battery{Id}";
            Capacity = capacity;
            StateCharge = stateCharge;
            SetPoint = setPoint;
            FrequentlyDisCharge = frequentlyDisCharge;
            TotalPower = 0;

            if (!CheckEmpty()) return;
            IsNeedToCharge = true;
            IsStartingChargeDischarge = false;
        }

        public double Capacity { get; } // kW
        public double StateCharge { get; private set; } // KW
        public double SetPoint { get; private set; } // Charge/Discharge control
        public double TotalPower { get;private set; }
        public double FrequentlyDisCharge { get; }
        public bool IsNeedToCharge { get; private set; }
        public bool IsStartingChargeDischarge { get; private set; }

        public static Battery Create(BatteryConfig batteryConfig)
        => new Battery(batteryConfig.Capacity, batteryConfig.StateCharge, batteryConfig.SetPoint, batteryConfig.FrequentlyDisCharge);
        private bool CheckEmpty()
        {
            return StateCharge < Capacity;
        }
        public void UpdateSetPoint()
        {
            SetPoint = new Random().NextDouble() * Capacity;
        }
        public async Task Charge()
        {
            IsStartingChargeDischarge = true;
            while (Math.Abs(Capacity - StateCharge) > 0.001)
            {
                StateCharge = Math.Max(0, Math.Min(Capacity, StateCharge + TotalPower));
                await Task.Delay(1000);
            }
            IsNeedToCharge = false;
            IsStartingChargeDischarge = false;
        }
        public async Task Discharge(double rateOfDischarge)
        {
            IsNeedToCharge = false;
            IsStartingChargeDischarge = true;
            while (StateCharge > SetPoint)
            {
                await Task.Delay(1000);
                if (StateCharge - rateOfDischarge < SetPoint)
                {
                    StateCharge = SetPoint; // Stop exactly at SetPoint
                }
                else
                {
                    StateCharge -= rateOfDischarge;
                }
            }
            IsNeedToCharge = true;
            IsStartingChargeDischarge = false;
        }
        public void SetTotalPower(double amount)
        {
            TotalPower=amount;
        }
    }
}
