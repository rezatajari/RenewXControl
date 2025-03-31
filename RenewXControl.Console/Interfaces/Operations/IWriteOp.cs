using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewXControl.Console.Interfaces.Operations
{
    /// <summary>
    /// Interface for write operations to be used by each asset in the application operation.
    /// </summary>
    public interface IWriteOp
    {
        /// <summary>
        /// Sets the Set Point (SP).
        /// </summary>
        /// <param name="value">Amount of set point</param>
        void SetSp(double value);
    }
}