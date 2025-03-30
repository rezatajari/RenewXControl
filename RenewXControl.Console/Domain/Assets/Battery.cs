using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenewXControl.Console.Interfaces;

namespace RenewXControl.Console.Domain.Assets
{
    public class Battery : Asset,IBattery
    {
        public double Capacity { get; set; } // kW
        public double StateOfCharge { get; set; } // %
        public double SetPoint { get; set; } // Charge/Discharge control
    }
}
