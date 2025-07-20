namespace RenewXControl.Application.DTOs.AssetMonitoring;

public record AssetDto(
    string AssetType,
    string Message,
    double SetPoint,
    DateTime Timestamp);