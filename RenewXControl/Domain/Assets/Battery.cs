using System.Net.Http.Headers;
using RenewXControl.Configuration.AssetsModel.Assets;

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
            ChargeStateMessage = "Battery is not connect to any of assets until now";

            if (!CheckEmpty()) return;
            IsNeedToCharge = true;
            IsStartingCharge = false;
        }

        public double Capacity { get; } // kW
        public double StateCharge { get; private set; } // KW
        public double SetPoint { get; private set; } // Charge/Discharge control
        public double FrequentlyDisCharge { get; }
        public string ChargeStateMessage { get; private set; }
        public bool IsNeedToCharge { get; private set; }
        public bool IsStartingCharge { get; private set; }

        public static Battery Create(BatteryConfig batteryConfig)
        => new Battery(batteryConfig.Capacity,batteryConfig.StateCharge,batteryConfig.SetPoint,batteryConfig.FrequentlyDisCharge);
        private bool CheckEmpty()
        {
            return StateCharge < Capacity;
        }
        public async Task Charge(double solarAp, double turbineAp)
        {
            IsStartingCharge = true;
            ChargeStateMessage = "Battery is charging";
            var totalPower = solarAp + turbineAp;
            while (Math.Abs(Capacity - StateCharge) > 0.001)
            {
                await Task.Delay(1000);
                StateCharge = Math.Max(0, Math.Min(Capacity, StateCharge + totalPower));
            }
            IsNeedToCharge = false;
            IsStartingCharge = false;
            ChargeStateMessage = "Charge complete.";
        }
        public async Task Discharge()
        {
            IsNeedToCharge = false;
            IsStartingCharge = true;
            ChargeStateMessage = "Battery is discharging";
            var amountToDischarge = StateCharge - SetPoint;
            var rateOfDischarge = amountToDischarge / FrequentlyDisCharge;

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
            ChargeStateMessage = "Discharge complete.";
        }
        public async Task ChargeDischarge(SolarPanel solarPanel,WindTurbine windTurbine)
        {
            switch (IsNeedToCharge)
            {
                // charging
                case true when IsStartingCharge == false:
                    solarPanel.SetSp();
                    solarPanel.PowerStatusMessage =
                        solarPanel.SetPoint != 0
                            ? "Solar is run.."
                            : "Solar is off.. we doesn't have good Irradiance";

                    windTurbine.SetSp();
                    windTurbine.PowerStatusMessage =
                        windTurbine.SetPoint != 0
                            ? "Turbine is run.."
                            : "Turbine is off.. we doesn't have good Wind speed";
                    _ = Task.Run(() => Charge(solarPanel.GetAp(), windTurbine.GetAp()));
                    break;

                // discharging
                case false when IsStartingCharge == false:
                    solarPanel.Off();
                    solarPanel.PowerStatusMessage = "Solar is off..";

                    windTurbine.Off();
                    windTurbine.PowerStatusMessage = "Turbine is off..";

                    SetSp();
                    _= Task.Run(Discharge);
                    break;
            }
        }
        public void SetSp()
         => SetPoint = new Random().NextDouble() * Capacity;
    }
}
