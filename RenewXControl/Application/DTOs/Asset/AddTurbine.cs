namespace RenewXControl.Application.DTOs.Asset
{
    public record AddTurbine(
        double WindSpeed,
        double SetPoint,
        double ActivePower
    );
}
