using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewXControl.Console.Interfaces
{
    internal interface IBattery
    {
        void UpdateSetPoint(double setPoint); // Charging or discharging
        void UpdateFrequency(double frequency); // Frequency of discharging energy
        void UpdateStateOfCharge(double SoC); // Update current charge state
    }
}
