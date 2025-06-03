namespace RenewXControl.Configuration.AssetsModel.Assets
{
    public record WindTurbineConfig    
    {
        public double SetPoint { get; set; }
        public double WindSpeed { get; set; }
        public double ActivePower { get; set; }
    }
}
