namespace RenewXControl.Api.DTOs;

public record Turbine(
    string AssetType,
    double WindSpeed,
    double ActivePower,
    string Message,
    double SetPoint,
    DateTime Timestamp)
    : Asset(AssetType,  Message, SetPoint, Timestamp);
