namespace RenewXControl.Api.DTOs
{
    public record MonitoringDto
    {
        public string AssetType { get; init; }         // "SolarPanel", "WindTurbine", "Battery"
        public string AssetName { get; init; }         // Asset name or identifier
        public double SensorValue { get; init; }       // Irradiance, WindSpeed, or Rate
        public double ActivePower { get; init; }       // Power output or battery charge/discharge power
        public double SetPoint { get; init; }          // Set point for control systems
        public double? StateCharge { get; init; }    // Battery-specific: SoC (%), null for others
        public double? RateDischarge { get; init; }             // Battery-specific: frequently updated rate, null for others
        public DateTime Timestamp { get; init; }       // Time of update
    }
}

