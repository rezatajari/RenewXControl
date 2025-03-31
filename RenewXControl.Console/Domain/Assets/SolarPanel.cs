using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RenewXControl.Console.InitConfiguration.AssetsModelConfig;

namespace RenewXControl.Console.Domain.Assets
{
    public class SolarPanel : Asset
    {
        private static int _id = 0;
        public SolarPanel(SolarPanelConfig solarConfig)
        {
            Id = _id++;
            Name = $"SP{Id}";
            Irradiance = solarConfig.Irradiance;
            ActivePower = solarConfig.ActivePower;
            SetPoint = solarConfig.SetPoint;
        }
        public double Irradiance { get; set; } // W/m²
        public double ActivePower { get; set; } // kW
        public double SetPoint { get; set; } // Determines operation status
        private static readonly Random RandomGenerator=new Random();


        public void Start()
        {
            Irradiance = GenerateIrradiance();

            if (Irradiance == 0.0 || SetPoint==0.0)
            {
                System.Console.WriteLine("Irradiance is zero or SetPoint is off. Stopping operation.");
                Stop();
            }
            System.Console.WriteLine("Solar is run...");
            ActivePower = Math.Min(Irradiance, SetPoint); // Ensure we don't exceed the solar power limit
        }

        public void Stop()
        {
            System.Console.WriteLine("Solar panel is turned off.");
            ActivePower = 0;
        }

        public void SetSp(double setPoint)
        {
            SetPoint = setPoint;
            System.Console.WriteLine($"SetPoint updated to {SetPoint} kW.");

            if (SetPoint == 0)
            {
                Stop();
            }
            else
            {
                ActivePower = Math.Min(Irradiance, SetPoint); // Update active power based on new set point
                System.Console.WriteLine($"Active power updated to {ActivePower} kW based on new set point.");
            }
        }

        public double GetAp()
        {
            if (SetPoint == 0) return 0;
            // With assumption Each irradiance equal active power
            ActivePower = Irradiance;
            return ActivePower;
        }

        private static double GenerateIrradiance()
        {
            return RandomGenerator.NextDouble()*10;
        }
    }
}
