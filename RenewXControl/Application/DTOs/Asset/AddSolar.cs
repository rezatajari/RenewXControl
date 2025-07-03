namespace RenewXControl.Application.DTOs.Asset
{
    public record AddSolar(
        double Irradiance,
        double SetPoint,
        double ActivePower
    );
}
