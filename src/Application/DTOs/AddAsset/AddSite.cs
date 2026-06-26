namespace Application.DTOs.AddAsset;

public record AddSite
{
    public string Name { get; init; }=string.Empty;
    public string Location { get; init; }=string.Empty;
}