using RenewXControl.Console.InitConfiguration.AssetsModelConfig.Users;

namespace RenewXControl.Console.Domain.Users
{
    public class User
    {
        private static int _id = 0;
        public User(UserConfig userConfig)
        {
            Id = ++_id;
            Name = userConfig.Name;
        }

        public void AddSite(Site site)
       => Sites.Add(site);
        
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Site> Sites { get; private set; } = [];
    }
}
