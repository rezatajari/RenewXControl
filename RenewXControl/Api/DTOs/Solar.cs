namespace RenewXControl.Api.DTOs;

public record Solar(
    string AssetType, 
    double Irradiance, 
    double ActivePower,
    string Message,
    double SetPoint,
    DateTime Timestamp)
    :Asset(AssetType,  Message, SetPoint, Timestamp);