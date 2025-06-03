using RenewXControl.Configuration.AssetsModel.Assets;

namespace RenewXControl.Domain.Assets
{
    public class SolarPanel : Asset
    {
        private static int _id = 0;
        public SolarPanel(SolarPanelConfig solarConfig) 
        {
            Id = ++_id;
            Name = $"SP{Id}";
            Irradiance = solarConfig.Irradiance;
            ActivePower = solarConfig.ActivePower;
            SetPoint = solarConfig.SetPoint;
            PowerStatusMessage = "Solar panel is not generating power now";
        }

        public double Irradiance { get; private set; } // W/m²
        public double ActivePower { get; private set; } // kW
        public double SetPoint { get; private set; } // Determines operation status
        public string PowerStatusMessage { get; set; }

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
