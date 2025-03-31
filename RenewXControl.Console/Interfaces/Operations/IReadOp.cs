using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewXControl.Console.Interfaces.Operations
{
    /// <summary>
    /// Interface for read operations to be used by each asset in the application operation.
    /// </summary>
    public interface IReadOp
    {
        /// <summary>
        /// Gets the Active Power (AP).
        /// </summary>
        /// <returns>The active power in kilowatts (kW).</returns>
        double GetSumAp();
    }
}