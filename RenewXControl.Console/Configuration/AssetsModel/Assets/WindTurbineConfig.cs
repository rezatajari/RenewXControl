namespace RenewXControl.Console.Configuration.AssetsModel.Assets
{
    public record WindTurbineConfig
    {
        public double WindSpeed { get; init; }
        public double SetPoint { get; init; }
        public double ActivePower { get; init; }


    }
}
