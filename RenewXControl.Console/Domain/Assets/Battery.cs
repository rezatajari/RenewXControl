using RenewXControl.Console.InitConfiguration.AssetsModelConfig;

namespace RenewXControl.Console.Domain.Assets
{
    public class Battery : Asset
    {
        private static int _id = 0;
        public Battery(BatteryConfig batteryConfig)
        {
            Id = ++_id;
            Name = $"Battery{Id}";
            Capacity = batteryConfig.Capacity;
            StateOfCharge = batteryConfig.StateOfCharge;
            SetPoint = batteryConfig.SetPoint;
            FrequentlyOfDisCharge = batteryConfig.FrequentlyOfDisCharge;
            ChargeStateMessage = "Battery is not connect to any of assets until now";

            if (SetPoint < Capacity && StateOfCharge < Capacity) IsNeedToCharge = true;
        }
        public double Capacity { get; } // kW
        public double StateOfCharge { get; private set; } // KW
        public double SetPoint { get; set; } // Charge/Discharge control
        public double FrequentlyOfDisCharge { get; }
        public string ChargeStateMessage { get; private set; }
        public bool IsNeedToCharge { get; private set; }
   
        public async Task Charge(double solarAp, double turbineAp)
        {
            IsNeedToCharge = true;
            ChargeStateMessage = "Battery is charging";
            var totalPower = solarAp + turbineAp;
            while (Math.Abs(Capacity - StateOfCharge) > 0.001)
            {
                await Task.Delay(1000);
                StateOfCharge = Math.Max(0, Math.Min(Capacity, StateOfCharge + totalPower));
            }
            ChargeStateMessage = "Charge complete.";
            IsNeedToCharge = false;
        }
        public async Task Discharge()
        {
            IsNeedToCharge = false;
            ChargeStateMessage = "Battery is discharging";
            var amountToDischarge = StateOfCharge - SetPoint;
            var rateOfDischarge = amountToDischarge / FrequentlyOfDisCharge;

            while (StateOfCharge > SetPoint)
            {
                await Task.Delay(1000);

                if (StateOfCharge - rateOfDischarge < SetPoint)
                {
                    StateOfCharge = SetPoint; // Stop exactly at SetPoint
                }
                else
                {
                    StateOfCharge -= rateOfDischarge;
                }
            }
            ChargeStateMessage = "Discharge complete.";
            IsNeedToCharge = true;
        }
        public void SetSp(double setPoint)
        {
            SetPoint = setPoint;
            System.Console.WriteLine($"SetPoint updated to {SetPoint} kW. Starting discharge process...");
            _ = Discharge();
        }
    }
}
