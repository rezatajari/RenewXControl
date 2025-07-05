namespace RenewXControl.Application.DTOs.AddAsset;
    public record AddSolar(
        double Irradiance,
        double SetPoint,
        double ActivePower
    );
