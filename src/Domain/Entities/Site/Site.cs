using Domain.Entities.Assets;
namespace Domain.Entities.Site;

public class Site
{
    private Site(){}
    private Site(string name, string location,Guid userId)
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
    public Guid UserId { get; set; }
    public static Site Create(string name,string location,Guid userId)
        => new Site(name, location,userId);
    public void AddAsset(Asset asset)
        => Assets.Add(asset);
}