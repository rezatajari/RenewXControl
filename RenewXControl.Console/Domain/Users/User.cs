using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenewXControl.Console.Configuration.AssetsModel.Users;

namespace RenewXControl.Console.Domain.Users
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
