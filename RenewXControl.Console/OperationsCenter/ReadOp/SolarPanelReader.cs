using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenewXControl.Console.Domain.Assets;

namespace RenewXControl.Console.OperationsCenter.ReadOp
{
    public class SolarPanelReader
    {
        private readonly SolarPanel _solarPanel;
        public SolarPanelReader(SolarPanel solarPanel)
        {
            _solarPanel = solarPanel;
        }

        public double GetIrradiance()
        {
            return _solarPanel.Irradiance;
        }
    }
}
