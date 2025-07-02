using Microsoft.AspNetCore.Identity;
using RenewXControl.Configuration.AssetsModel.Users;

namespace RenewXControl.Domain.Users
{
    public sealed class User:IdentityUser
    {
        private User(){}
        private User(string username)
        {
            UserName = username;
            CreateTime = DateTime.UtcNow;
        }

        public DateTime CreateTime { get; set; } 

        public ICollection<Site> Sites { get; private set; } = [];
   
        public static User Create(string name)
        {
            return new User(name);
        }

        public void ChangeName(string newUsername)
        {
            UserName = newUsername;
        }

        public void AddSite(Site site) => Sites.Add(site);
    }
}
