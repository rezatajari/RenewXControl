using RenewXControl.Configuration.AssetsModel.Assets;

namespace RenewXControl.Domain.Assets
{
    public class WindTurbine : Asset
    {
        private WindTurbine() { }
        private WindTurbine(double windSpeed,double activePower,double setPoint) 
        {
            Name = $"WT{Id}";
            WindSpeed = windSpeed;
            ActivePower = activePower;
            SetPoint = setPoint;
            PowerStatusMessage = "Wind turbine is not generating power now";
        }

        public double WindSpeed { get;private set; }  // km/h
        public double ActivePower { get;private set; } // kW
        public double SetPoint { get;private set; } // Determines operation status
        public string PowerStatusMessage { get;  set; }

        public static WindTurbine Create(WindTurbineConfig turbineConfig)
            => new WindTurbine(turbineConfig.WindSpeed, turbineConfig.ActivePower, turbineConfig.SetPoint);
        public void Start()
        {
            if (WindSpeed == 0.0 || SetPoint == 0.0)
            {
                ActivePower = 0;
                PowerStatusMessage = "Wind turbine is not generating power";
            }
            else
            {
                PowerStatusMessage = "Wind turbine is generating power";
            }
          

        }
        public void Off()
        {
            SetPoint = 0;
            ActivePower = SetPoint;
        }
        public void SetSp()
        {
            SetPoint = new Random().NextDouble() * WindSpeed;
            if (SetPoint != 0)
            {
                ActivePower = SetPoint;
            }
            else
            {
                Off();
            }
        }

        public double GetAp()
        {
            ActivePower = Math.Min(WindSpeed, SetPoint);
            return ActivePower;
        }
        public double GetWindSpeed()
        {
            WindSpeed = new Random().NextDouble() * 10;
            return WindSpeed;
        }
    }
}
