using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewXControl.Console.Domain.Assets
{
    // It's good to mark your base classes with 'abstract' modifier so, these classes can not be initialized directly.
    // We never use them as a stand-alone object in out logic. We just use them to share some functionalities.
    // Base class for all assets
    public class Asset
    {
        public Asset(int siteId)
        {
            SiteId = siteId;
        }

        public int Id { get; set; }

        // Asset Should not have SiteId in them based on our domain
        // The site already has a list of known assets. This bidirectional relationship is unnecessary here.
        // Imagine some day you want to move one of your assets from site A to site B and you need to change both the site entity and the asset entity
        // While you only needed to remove the asset from site A and add it to site B
        public int SiteId { get; set; }
        public string Name { get; set; }
    }
}
