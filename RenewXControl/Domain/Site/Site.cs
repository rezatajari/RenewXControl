using RenewXControl.Application.DTOs.AddAsset;
using RenewXControl.Configuration.AssetsModel.Users;
using RenewXControl.Domain.Assets;

namespace RenewXControl.Domain.Site
{
    public class Site
    {
        private Site(){}
        private Site(string name, string location,string userId)
        {
            Name = name;
            Location = location;
            UserId = userId;
        }

        public Guid Id { get;private set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        public ICollection<Asset> Assets { get; private set; } = [];
        public string UserId { get; set; }
        public User.User User { get; set; }

        public static Site Create(AddSite addSite,string userId)
        => new Site(addSite.Name, addSite.Location,userId);
        public void AddAsset(Asset asset)
        => Assets.Add(asset);
    }
}
