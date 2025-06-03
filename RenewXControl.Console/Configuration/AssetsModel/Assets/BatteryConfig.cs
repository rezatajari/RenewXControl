namespace RenewXControl.Console.Configuration.AssetsModel.Assets
{
    public record BatteryConfig
    {
        public double Capacity { get; init; }
        public double StateOfCharge { get; init; }
        public double SetPoint { get; init; }
        public double FrequentlyOfDischarge { get; init; }
    }
}
