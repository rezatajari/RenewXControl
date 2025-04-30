using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenewXControl.Console.Domain.Assets;
using RenewXControl.Console.InitConfiguration.AssetsModelConfig.Users;

namespace RenewXControl.Console.Domain.Users
{
    public class Site
    {
        public Site(SiteConfig siteConfig)
        {
            Name = siteConfig.Name;
            Location = siteConfig.Location;
        }
        public string Name { get; set; }
        public string Location { get; set;}
        public List<Asset> Assets { get;private set; } = [];

        public void AddAsset(Asset  asset) 
        => Assets.Add(asset);
    }
}
