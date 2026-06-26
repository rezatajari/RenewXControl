namespace Domain.Entities.Assets;

public class Battery : Asset
{
    private Battery() { }
    private Battery(double capacity, double stateCharge, double setPoint, double frequentlyDisCharge, Guid siteId)
    {
        FrequentlyDisCharge = frequentlyDisCharge;
        Name = $"Battery{Id}";
        Capacity = capacity;
        StateCharge = stateCharge;
        SetPoint = setPoint;
        TotalPower = 0;
        SiteId= siteId;

        if (!NeedsCharging()) return;
        IsNeedToCharge = true;
        IsStartingChargeDischarge = false;
    }

    public double Capacity { get; private set; } // kW
    public double StateCharge { get; private set; } // KW
    public double SetPoint { get; private set; } // Charge/Discharge control
    public double TotalPower { get;private set; }
    public double FrequentlyDisCharge { get; }
    public bool IsNeedToCharge { get; private set; }
    public bool IsStartingChargeDischarge { get; private set; }
    public BatteryState State { get; private set; }

    public static Battery Create(double capacity,
            double stateCharge,
            double setPoint,
            double frequentlyDischarge,
            Guid siteId)
        => new Battery(capacity, stateCharge, setPoint, frequentlyDischarge, siteId);
    private bool NeedsCharging()
    {
        return StateCharge < Capacity;
    }
    public void UpdateSetPoint()
    {
        SetPoint = new Random().NextDouble() * Capacity;
    }
    public async Task Charge()
    {
        State = BatteryState.Charging;
        IsStartingChargeDischarge = true;
        while (Math.Abs(Capacity - StateCharge) > 0.001)
        {
            StateCharge = Math.Max(0, Math.Min(Capacity, StateCharge + TotalPower));
            await Task.Delay(1000);
        }
        IsNeedToCharge = false;
        IsStartingChargeDischarge = false;
        State = BatteryState.ChargingCompleted;
        UpdateSetPoint();
    }
    public async Task Discharge()
    {
        State = BatteryState.Discharging;
        var amountToDischarge = StateCharge - SetPoint;
        var rateOfDischarge = amountToDischarge / FrequentlyDisCharge;
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
        State = BatteryState.DischargingCompleted;
    }
    public void SetTotalPower(double amount)
    {
        TotalPower=amount;
    }
}