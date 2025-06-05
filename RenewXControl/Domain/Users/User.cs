using RenewXControl.Configuration.AssetsModel.Users;

namespace RenewXControl.Domain.Users
{
    public sealed class User
    {
        private User(){}
        private User(string name)
        {
            Name = name;
        }

        public Guid Id { get;private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        public ICollection<Site> Sites { get; private set; } = [];
   
        public static User Create(string name)
        {
            return new User(name);
        }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void AddSite(Site site) => Sites.Add(site);
    }
}
