using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenewXControl.Console.Interfaces;

namespace RenewXControl.Console.Domain.Assets
{
    public class WindTurbine : Asset,IWindTurbine
    {
        public double WindSpeed { get; set; }  // km/h
        public double ActivePower { get; set; } // kW
        public double SetPoint { get; set; } // Determines operation status

        public void Start()
        {
        }

        public void Stop()
        {
        }

        public void UpdateWindSpeed() { }

        public void UpdateSetPoint() { }

    }
}
