using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewXControl.Console.Domain.Assets
{
    public class WindTurbine : Asset
    {
        public double WindSpeed { get; set; }  // km/h
        public double ActivePower { get; set; } // kW
        public double SetPoint { get; set; } // Determines operation status
        private static readonly Random RandomGenerator = new Random();

        public void Start()
        {
            WindSpeed = GeneratorWindSpeed();

            if (WindSpeed == 0.0 || SetPoint == 0.0)
            {
                System.Console.WriteLine("WindSpeed is zero or SetPoint is off. Stopping operation.");
                Stop();
            }

            System.Console.WriteLine($"Wind turbine started. Current wind speed: {WindSpeed} km/h");

            ActivePower = Math.Min(WindSpeed, SetPoint); // Ensure we don't exceed the wind power limit

        }

        public void Stop()
        {
            System.Console.WriteLine("Wind turbine is turned off.");
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
                ActivePower = Math.Min(WindSpeed, SetPoint); // Update active power based on new set point
                System.Console.WriteLine($"Active power updated to {ActivePower} kW based on new set point.");
            }
        }

        public double GetAp()
        {
            return ActivePower;
        }

        private static double GeneratorWindSpeed()
        {
            return RandomGenerator.NextDouble() * 10;
        }
    }
}
