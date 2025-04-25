using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewXControl.Console.InitConfiguration.AssetsModelConfig.Assets
{
    // A config object is not supposed to have any behavior. It only carries some read-only data
    // So, it is better to define it as a record class. Please investigate about record, struct and class in C#
    public class BatteryConfig
    {
        public double Capacity { get; set; }
        public double StateOfCharge { get; set; }
        public double SetPoint { get; set; }
        public double FrequentlyOfDisCharge { get; set; }
    }
}
