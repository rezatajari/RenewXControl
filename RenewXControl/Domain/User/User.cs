using Microsoft.AspNetCore.Identity;

namespace RenewXControl.Domain.User
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
        public string? ProfileImage { get;private set; }

        public ICollection<Site.Site> Sites { get; private set; } = [];
   
        public static User Create(string username,string email)
        {
            return new User(username,email);
        }

        public void ChangeProfile(string newName, string? newImagePath)
        {
            UserName=newName;
            ProfileImage=newImagePath;
        }
    }
}
