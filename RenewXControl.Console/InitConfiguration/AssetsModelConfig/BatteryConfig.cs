using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewXControl.Console.InitConfiguration.AssetsModelConfig
{
    public class BatteryConfig
    {
        public double Capacity { get; set; } 
        public double StateOfCharge { get; set; } 
        public double SetPoint { get; set; }
        public double FrequentlyOfDisCharge { get; set; }
    }
}
