using RenewXControl.Console.InitConfiguration.AssetsModelConfig;

namespace RenewXControl.Console.Domain.Assets
{
    public class WindTurbine : Asset
    {
        private static int _id=0;
        public WindTurbine(WindTurbineConfig turbineConfig)
        {
            Id = ++_id;
            Name = $"WT{Id}";
            WindSpeed = turbineConfig.WindSpeed;
            ActivePower = turbineConfig.ActivePower;
            SetPoint = turbineConfig.SetPoint;
            PowerStatusMessage = "Wind turbine is not generating power";
        }
        public double WindSpeed { get;private set; }  // km/h
        public double ActivePower { get;private set; } // kW
        public double SetPoint { get;private set; } // Determines operation status
        public string PowerStatusMessage { get; private set; }

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
        public void SetSp(double setPoint)
        {
            SetPoint = setPoint;
            PowerStatusMessage = "Solar panel is set base on new update set point";
            ActivePower = Math.Min(WindSpeed, SetPoint); // Update active power based on new set point
        }
        public double GetAp()
        {
            ActivePower = Math.Min(WindSpeed, SetPoint);
            return ActivePower;
        }
        public double GetWindSpeed()
        {
            var randomGen = new Random();
            WindSpeed = randomGen.NextDouble() * 10;
            return WindSpeed;
        }
    }
}
