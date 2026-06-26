namespace Domain.Entities.Assets;

public abstract class Asset
{
    public Guid Id { get; set; }= Guid.NewGuid();
    public Guid SiteId { get; set; }
    public string Name { get; set; }=string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime UpdateTime { get; set; }
    public DateTime CreateTime { get; set; } 

    public Site Site { get; set; }= null!;

}