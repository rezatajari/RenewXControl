namespace Domain.Entities.Assets;

public abstract  class Asset
{
    public Guid Id { get; set; }= Guid.NewGuid();
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime UpdateTime { get; set; }
    public DateTime CreateTime { get; set; } = DateTime.UtcNow;

    public Guid SiteId { get; set; }
    public Site.Site Site { get; set; }

}