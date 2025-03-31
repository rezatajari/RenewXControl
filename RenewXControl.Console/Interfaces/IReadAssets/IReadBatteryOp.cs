using RenewXControl.Console.Interfaces.Operations;


namespace RenewXControl.Console.Interfaces.IReadAssets;

/// <summary>
/// Interface for read operations specific to battery assets.
/// </summary>
public interface IReadBatteryOp : IReadOp
{
    /// <summary>
    /// Gets the State of Charge (SoC) as a percentage of kilowatts (kW).
    /// </summary>
    /// <returns>The state of charge percentage.</returns>
    double GetSoC();

    /// <summary>
    /// Gets the capacity of the battery.
    /// </summary>
    /// <returns>The capacity of the battery in kilowatt-hours (kWh).</returns>
    double GetCapacity();
}
