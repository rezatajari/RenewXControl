using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenewXControl.Console.Configuration.AssetsModel.Users;

namespace RenewXControl.Console.Domain.Users
{
    // It's good to marke entity classes with 'seald' access modifier so, no one can inherit and change its behaviours
    public class User
    {
        // Id should not be static! you will have different instance of an entity and each entity should have its own Id
        // The best practice here is to have a base class named BaseEntity and have the Id in it
        // This way you can keep it out of your entity class and share it with all others
        // Sice the Id is an I/O concern (related to databae) and not our bussiness logic concern.
        private static int _id = 0;

        // An entity should expose all its behaviors using well descriptive methods.
        // Better to have constructors private and initialize the entity using a static method. Something like User.Create(...)
        // So, it is very clear to any clinet how they are initializing the entity because, sometimes you need to initialize an entity with different parametters for different purposes


        // It is also not a good practice to send a complex object as parametter (UserConfig in the below constructor). Better to explicitly have the primary value types as parameter like public User(string name)

        // One other thing is, the entity class should always stay pure. It should know nothing about the technologies and tools and configuration that we have.
        // They are our core bussiness logic and should not be concerend about any I/O or other application level concerns. We will talk about this later while learning about domain modeling methods
        public User(UserConfig userConfig)
        {
            Id = ++_id;
            Name = userConfig.Name;
        }

        public void AddSite(Site site) => Sites.Add(site);

        // Entity must be encapsulated. It means, no one can modify it directly from outside. All ptopertoes have private setter
        // If a client needs to edit the name, we should expose a method named ChangeName(string name) or something to give them the ability to update the name but,
        // This way we can guaranty our rules and validation and reject bad requests based on our bussiness logic. For example we rehject a name if it has numbers in it.
        // Please investigate more about Encapsulation in OOP and how we implement it in C#
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Site> Sites { get; private set; } = [];
    }
}
