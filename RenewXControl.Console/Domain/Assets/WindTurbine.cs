using RenewXControl.Console.Configuration.AssetsModel.Assets;

namespace RenewXControl.Console.Domain.Assets
{
    public class WindTurbine : Asset
    {
        private static int _id=0;
        public WindTurbine(WindTurbineConfig turbineConfig,int siteId) : base(siteId)
        {
            Id = ++_id;
            SiteId=siteId;
            Name = $"WT{Id}";
            WindSpeed = turbineConfig.WindSpeed;
            ActivePower = turbineConfig.ActivePower;
            SetPoint = turbineConfig.SetPoint;
            PowerStatusMessage = "Wind turbine is not generating power now";
        }

        public double WindSpeed { get;private set; }  // km/h
        public double ActivePower { get;private set; } // kW
        public double SetPoint { get;private set; } // Determines operation status
        public string PowerStatusMessage { get;  set; }

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
