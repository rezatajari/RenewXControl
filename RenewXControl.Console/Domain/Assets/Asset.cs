using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewXControl.Console.Domain.Assets
{
    // Base class for all assets
    public class Asset
    {
        public Asset(int siteId)
        {
            SiteId=siteId;
        }
        public int Id { get; set; }
        public int SiteId { get; set; }
        public string Name { get; set; }
    }
}
