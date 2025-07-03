using Microsoft.AspNetCore.Identity;
using RenewXControl.Configuration.AssetsModel.Users;

namespace RenewXControl.Domain.Users
{
    public sealed class User:IdentityUser
    {
        private User(){}
        private User(string username, string email)
        {
            UserName = username;
            Email=email;
            CreateTime = DateTime.UtcNow;
        }

        public DateTime CreateTime { get; set; } 

        public ICollection<Site> Sites { get; private set; } = [];
   
        public static User Create(string username,string email)
        {
            return new User(username,email);
        }

        public void ChangeName(string newUsername)
        {
            UserName = newUsername;
        }

        public void AddSite(Site site) => Sites.Add(site);
    }
}
