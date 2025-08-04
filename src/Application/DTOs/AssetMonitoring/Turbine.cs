namespace Application.DTOs.AssetMonitoring;

public record Turbine(
    string AssetType,
    double WindSpeed,
    double ActivePower,
    string Message,
    double SetPoint,
    DateTime Timestamp)
    : AssetDto(AssetType,  Message, SetPoint, Timestamp);
