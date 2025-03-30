using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewXControl.Console.Interfaces
{
    internal interface ISolarPanel
    {
        void Start();
        void Stop();
        void UpdateSetPoint(double setPoint);
        void CalculateActivePower();
    }
}
