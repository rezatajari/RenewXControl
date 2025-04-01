using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenewXControl.Console.Domain.Assets;

namespace RenewXControl.Console.OperationsCenter.ReadOp
{
    public class WindTurbineReader
    {
        private readonly WindTurbine _windTurbine;

        public WindTurbineReader(WindTurbine windTurbine)
        {
            _windTurbine = windTurbine;
        }

        public double GetWs()
        {
            return _windTurbine.WindSpeed;
        }
    }
}
