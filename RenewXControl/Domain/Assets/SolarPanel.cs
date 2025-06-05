using RenewXControl.Configuration.AssetsModel.Assets;

namespace RenewXControl.Domain.Assets
{
    public class SolarPanel : Asset
    {
        private SolarPanel() { }
        private SolarPanel(double irradiance,double activePower,double setPoint) 
        {
            Name = $"SP{Id}";
            Irradiance = irradiance;
            ActivePower =activePower;
            SetPoint = setPoint;
            PowerStatusMessage = "Solar panel is not generating power now";
        }

        public double Irradiance { get; private set; } // W/m²
        public double ActivePower { get; private set; } // kW
        public double SetPoint { get; private set; } // Determines operation status
        public string PowerStatusMessage { get; set; }

        public static SolarPanel Create(SolarPanelConfig solarConfig)
            => new SolarPanel(solarConfig.Irradiance,solarConfig.ActivePower,solarConfig.SetPoint);
        public void Start()
        {
            if (Irradiance == 0.0 || SetPoint == 0.0)
            {
                ActivePower = 0;
                PowerStatusMessage = "Solar panel is not generating power";
            }
            else
            {
                PowerStatusMessage = "Solar panel is generating power";
            }

        }
        public void Off()
        {
            SetPoint = 0;
            ActivePower = SetPoint;
        }
        public void SetSp()
        {
            SetPoint = new Random().NextDouble() * Irradiance;
            if (SetPoint != 0){
                ActivePower = SetPoint;
            }
            else
            {
                Off();
            }
        }
        public double GetAp()
        {
            ActivePower = Math.Min(Irradiance, SetPoint);

            return ActivePower;
        }
        public double GetIrradiance()
        {
            Irradiance = new Random().NextDouble() * 10;
            return Irradiance;
        }

    }
}
