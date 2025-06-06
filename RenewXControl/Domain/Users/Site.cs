﻿using RenewXControl.Configuration.AssetsModel.Users;
using RenewXControl.Domain.Assets;

namespace RenewXControl.Domain.Users
{
    public class Site
    {
        private Site(){}
        private Site(string name, string location)
        {
            Name = name;
            Location = location;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        public ICollection<Asset> Assets { get; private set; } = [];
        public Guid UserId { get; set; }
        public User User { get; set; }

        public static Site Create(SiteConfig siteConfig)
        => new Site(siteConfig.Name, siteConfig.Location);
        public void AddAsset(Asset asset)
        => Assets.Add(asset);
    }
}
