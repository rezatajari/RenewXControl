namespace RenewXControl.Application.DTOs.Asset
{
    public record AddBattery(
        double Capacity,
        double StateCharge,
        double SetPoint,
        double FrequentlyDischarge
    );
}
