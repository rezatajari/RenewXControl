using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenewXControl.Console.Domain.Assets;

namespace RenewXControl.Console.Domain.Users
{
    public class Site
    {
        public string Id { get; set; }
        public string Location { get; set;}
        public List<Asset> Assets { get; set; } = [];
    }
}
