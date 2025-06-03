using RenewXControl.Configuration.AssetsModel.Users;
using RenewXControl.Domain.Assets;

namespace RenewXControl.Domain.Users
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
