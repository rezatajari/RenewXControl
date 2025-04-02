using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewXControl.Console.InitConfiguration.AssetsModelConfig
{
    public class SolarPanelConfig
    {
        public double Irradiance { get; set; }
        public double SetPoint { get; set; }
        public double ActivePower { get; set; }
    }
}
