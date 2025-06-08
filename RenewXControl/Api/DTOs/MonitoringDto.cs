namespace RenewXControl.Api.DTOs
{
    public record MonitoringDto
    {
        public string AssetType { get; init; } // "SolarPanel", "WindTurbine"
        public string AssetName { get; init; } //  asset name or identifier
        public double SensorValue { get; init; } // irradiance or wind speed
        public double ActivePower { get; init; } // Power output
        public double SetPoint { get; set; } //  set point for control systems
        public DateTime Timestamp { get; init; } //  time of update
    }

}
