namespace RenewXControl.Configuration.AssetsModel.Assets
{
    public record BatteryConfig
    {
        public double Capacity { get; init; }
        public double StateOfCharge { get; init; }
        public double SetPoint { get; init; }
        public double FrequentlyOfDisCharge { get; init; }
    }
}
