namespace Application.DTOs.AssetMonitoring;

public record BatteryDto(
    string AssetType,
    double Capacity,
    double? StateCharge,
    double? RateDischarge,
    string Message,
    double SetPoint,
    DateTime Timestamp)
    : AssetDto(AssetType, Message, SetPoint, Timestamp);
