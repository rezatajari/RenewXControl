using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenewXControl.Console.Domain.Assets;

namespace RenewXControl.Console.OperationsCenter.ReadOp
{
    public class BatteryReader 
    {
        private readonly Battery _battery;
        public BatteryReader(Battery battery)
        {
            _battery = battery;
        }

        public string GetName()
        {
            return _battery.Name;
        }

        public double GetSoC()
        {
         return _battery.StateOfCharge;
        }
    }
}
