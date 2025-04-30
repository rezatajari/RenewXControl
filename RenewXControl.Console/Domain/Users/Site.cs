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
        private static int _id = 0;
        public Site(SiteConfig siteConfig)
        {
            Id = ++_id;
            Name = siteConfig.Name;
            Location = siteConfig.Location;
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Location { get; set;}
        public List<Asset> Assets { get;private set; } = [];

        public void AddAsset(Asset  asset) 
        => Assets.Add(asset);
    }
}
