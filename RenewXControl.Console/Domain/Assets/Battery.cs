using RenewXControl.Console.InitConfiguration.AssetsModelConfig;

namespace RenewXControl.Console.Domain.Assets
{
    public class Battery : Asset
    {
        private static int _id = 0;
        public Battery(BatteryConfig batteryConfig)
        {
            Id = _id++;
            Name = $"Battery{Id}";
            Capacity = batteryConfig.Capacity;
            StateOfCharge = batteryConfig.StateOfCharge;
            SetPoint = batteryConfig.SetPoint;
            FrequentlyOfDisCharge = batteryConfig.FrequentlyOfDisCharge;
        }
        public double Capacity { get; set; } // kW
        public double StateOfCharge { get; set; } // KW
        public double SetPoint { get; set; } // Charge/Discharge control
        public double FrequentlyOfDisCharge { get; set; }

        public  void SetSp(double setPoint)
        {
            SetPoint = setPoint;
            System.Console.WriteLine($"SetPoint updated to {SetPoint} kW. Starting discharge process...");
            _ = Discharge();
        }

        public void UpdateSoC(double value)
        {
            StateOfCharge = Math.Max(0, Math.Min(Capacity, StateOfCharge + value));
        }

        public void Charge()
        {
            System.Console.WriteLine("Battery is start to charge");
        }

        public async Task Discharge()
        {
            var amountToDischarge = StateOfCharge - SetPoint;
            var rateOfDischarge = amountToDischarge / FrequentlyOfDisCharge;

            while (StateOfCharge > SetPoint)
            {
                await Task.Delay(1000);
                StateOfCharge -= rateOfDischarge;

                System.Console.WriteLine($"Battery discharged by {rateOfDischarge} kW. Current charge: {StateOfCharge} kW.");
            }

            System.Console.WriteLine($"Battery reached set point {SetPoint} kW. Discharge complete.");
         }
    }
}
