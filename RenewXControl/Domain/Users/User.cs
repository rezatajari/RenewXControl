using RenewXControl.Configuration.AssetsModel.Users;

namespace RenewXControl.Domain.Users
{
    public sealed class User
    {
        public string Name { get; private set; }
        private User(string name)
        {
            Name = name;
        }
        public static User Create(string name)
        {
            return new User(name);
        }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void AddSite(Site site) => Sites.Add(site);
        public List<Site> Sites { get; private set; } = [];
    }
}
