using Application.DTOs.AssetMonitoring;

namespace Application.DTOs.AssetMonitoring;

public record Solar(
    string AssetType, 
    double Irradiance, 
    double ActivePower,
    string Message,
    double SetPoint,
    DateTime Timestamp)
    :AssetDto(AssetType,  Message, SetPoint, Timestamp);