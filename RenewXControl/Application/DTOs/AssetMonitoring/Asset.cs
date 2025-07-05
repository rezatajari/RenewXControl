namespace RenewXControl.Application.DTOs.AssetMonitoring;

public record Asset(
    string AssetType,
    string Message,
    double SetPoint,
    DateTime Timestamp);