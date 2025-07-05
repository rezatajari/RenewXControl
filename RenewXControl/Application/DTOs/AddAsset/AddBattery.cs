namespace RenewXControl.Application.DTOs.AddAsset
{
    public record AddBattery(
        double Capacity,
        double StateCharge,
        double SetPoint,
        double FrequentlyDischarge
    );
}
