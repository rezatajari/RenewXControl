using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenewXControl.Console.Interfaces;

namespace RenewXControl.Console.Domain.Assets
{
    public class SolarPanel : Asset,ISolarPanel
    {
        public double Irradiance { get; set; } // W/m²
        public double WindSpeed { get; set; } // km/h
        public double ActivePower { get; set; } // kW
        public double SetPoint { get; set; } // Determines operation status
    }
}
