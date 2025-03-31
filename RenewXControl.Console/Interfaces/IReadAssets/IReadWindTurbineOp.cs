using RenewXControl.Console.Interfaces.Operations;

namespace RenewXControl.Console.Interfaces.IReadAssets;

/// <summary>
/// Interface for read operations specific to wind turbine assets.
/// </summary>
public interface IReadWindTurbineOp : IReadOp
{
    /// <summary>
    /// Gets the Wind Speed (WS).
    /// </summary>
    /// <returns>The wind speed in meters per second (m/s).</returns>
    double GetWs();
}
