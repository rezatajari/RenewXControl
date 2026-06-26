namespace Application.DTOs.AssetMonitoring;

public record UserMonitoringInfoDto(
    string? Username,
    string? SiteName,
    string? SiteLocation,
    Solar Solar,
    Turbine Turbine,
    BatteryDto Battery);
