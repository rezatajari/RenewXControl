namespace RenewXControl.Configuration.AssetsModel.Assets
{
    public record SolarPanelConfig
    {
        public double Irradiance { get; set; }
        public double SetPoint { get; set; }
        public double ActivePower { get; set; }
    }
}
