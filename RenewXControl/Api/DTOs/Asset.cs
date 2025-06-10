namespace RenewXControl.Api.DTOs;

public record Asset(
    string AssetType,
    string Message,
    double SetPoint,
    DateTime Timestamp);
