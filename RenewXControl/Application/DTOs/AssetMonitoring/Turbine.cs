namespace RenewXControl.Application.DTOs.AssetMonitoring;

public record Turbine(
    string AssetType,
    double WindSpeed,
    double ActivePower,
    string Message,
    double SetPoint,
    DateTime Timestamp)
    : AssetMonitoring.AssetDto(AssetType,  Message, SetPoint, Timestamp);
