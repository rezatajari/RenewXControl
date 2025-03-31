using RenewXControl.Console.Domain.Assets;
using RenewXControl.Console.Interfaces.IReadAssets;

namespace RenewXControl.Console.OperationsCenter
{
    public class Read : IReadBatteryOp, IReadWindTurbineOp, IReadSolarPanelOp
    {
        private readonly Battery _battery;
        private readonly WindTurbine _windTurbine;
        private readonly SolarPanel _solarPanel;
       
        public Read(Battery battery, WindTurbine windTurbine, SolarPanel solarPanel)
        {
            _battery = battery;
            _windTurbine= windTurbine;
            _solarPanel = solarPanel;
        }
       
        public double GetSumAp()
        {
          return _windTurbine.GetAp()+ _solarPanel.GetAp();
        }

        public double GetWs()
        {
            return _windTurbine.WindSpeed;
        }
        public double GetSoC()
        {
           return _battery.StateOfCharge;
        }

        public double GetCapacity()
        {
            return _battery.Capacity;
        }
    }
}
