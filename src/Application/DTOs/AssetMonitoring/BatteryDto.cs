namespace Application.DTOs.AssetMonitoring;

public record BatteryDto(
    string AssetType,
    string Message,
    double Capacity,
    double SetPoint,
    double? StateCharge,
    double? RateDischarge,
    DateTime Timestamp)
    : AssetDto(AssetType, Message, SetPoint, Timestamp);
