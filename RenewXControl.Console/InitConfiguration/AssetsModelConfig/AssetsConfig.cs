using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewXControl.Console.InitConfiguration.AssetsModelConfig
{
    public class AssetsConfig
    {
        public WindTurbineConfig WindTurbineConfig { get; set; }
        public SolarPanelConfig SolarPanelConfig { get; set; }
        public BatteryConfig BatteryConfig { get; set; }
    }
}
