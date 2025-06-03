using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewXControl.Console.Domain.Assets
{
    public abstract class Asset
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
