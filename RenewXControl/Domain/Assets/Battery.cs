using RenewXControl.Configuration.AssetsModel.Assets;
using RenewXControl.Domain.Interfaces.Assets;


namespace RenewXControl.Domain.Assets
{
    public class Battery : Asset
    {
        private Battery(){}
        private Battery(double capacity,double stateCharge,double setPoint,double frequentlyDisCharge) 
        {
            FrequentlyDisCharge = frequentlyDisCharge;
            Name = $"Battery{Id}";
            Capacity = capacity;
            StateCharge = stateCharge;
            SetPoint = setPoint;
            FrequentlyDisCharge = frequentlyDisCharge;

            if (!CheckEmpty()) return;
            IsNeedToCharge = true;
            IsStartingCharge = false;
        }

        public double Capacity { get; } // kW
        public double StateCharge { get; private set; } // KW
        public double SetPoint { get; private set; } // Charge/Discharge control
        public double FrequentlyDisCharge { get; }
        public bool IsNeedToCharge { get; private set; }
        public bool IsStartingCharge { get; private set; }

        public static Battery Create(BatteryConfig batteryConfig)
        => new Battery(batteryConfig.Capacity,batteryConfig.StateCharge,batteryConfig.SetPoint,batteryConfig.FrequentlyDisCharge);
        private bool CheckEmpty()
        {
            return StateCharge < Capacity;
        }
        public void UpdateSetPoint()
        {
            SetPoint =  new Random().NextDouble() * Capacity;
        }
        public async Task Charge(double totalPower)
        {
            IsStartingCharge = true;
            while (Math.Abs(Capacity - StateCharge) > 0.001)
            {
                await Task.Delay(1000);
                StateCharge = Math.Max(0, Math.Min(Capacity, StateCharge + totalPower));
            }
            IsNeedToCharge = false;
            IsStartingCharge = false;
        }
        public async Task Discharge(double rateOfDischarge)
        {
            IsNeedToCharge = false;
            IsStartingCharge = true;
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
            IsStartingCharge = false;
            IsNeedToCharge = true;
        }
    }
}
