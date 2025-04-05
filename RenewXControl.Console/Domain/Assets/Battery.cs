using RenewXControl.Console.InitConfiguration.AssetsModelConfig.Assets;

namespace RenewXControl.Console.Domain.Assets
{
    public class Battery : Asset
    {
        private static int _id = 0;
        public Battery(BatteryConfig batteryConfig, int siteId) : base(siteId)
        {
            Id = ++_id;
            SiteId=siteId;
            Name = $"Battery{Id}";
            Capacity = batteryConfig.Capacity;
            StateOfCharge = batteryConfig.StateOfCharge;
            SetPoint = batteryConfig.SetPoint;
            FrequentlyOfDisCharge = batteryConfig.FrequentlyOfDisCharge;
            ChargeStateMessage = "Battery is not connect to any of assets until now";

            if (!CheckEmpty()) return;
            IsNeedToCharge=true;
            IsStartingCharge = false;
        }

        public double Capacity { get; } // kW
        public double StateOfCharge { get; private set; } // KW
        public double SetPoint { get; set; } // Charge/Discharge control
        public double FrequentlyOfDisCharge { get; }
        public string ChargeStateMessage { get; private set; }
        public bool IsNeedToCharge { get; set; }
        public bool IsStartingCharge{ get; set; }

        private bool CheckEmpty()
        {
            return StateOfCharge < Capacity;
        }
        public async Task Charge(double solarAp, double turbineAp)
        {
            IsStartingCharge = true;
            ChargeStateMessage = "Battery is charging";
            var totalPower = solarAp + turbineAp;
            while (Math.Abs(Capacity - StateOfCharge) > 0.001)
            {
                await Task.Delay(1000);
                StateOfCharge = Math.Max(0, Math.Min(Capacity, StateOfCharge + totalPower));
            }
            IsNeedToCharge = false;
            IsStartingCharge = false;
            ChargeStateMessage = "Charge complete.";
        }
        public async Task Discharge()
        {
            IsNeedToCharge=false;
            IsStartingCharge = true;
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
            IsStartingCharge= false;
            IsNeedToCharge = true;
            ChargeStateMessage = "Discharge complete.";
        }
        public void SetSp()
        {
            SetPoint= new Random().NextDouble() * Capacity;
        }
    }
}
