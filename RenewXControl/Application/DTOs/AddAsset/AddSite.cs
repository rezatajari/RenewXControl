using System.ComponentModel.DataAnnotations;

namespace RenewXControl.Application.DTOs.AddAsset;

/// <summary>
/// Data required to add a new site.
/// </summary>
public record AddSite
{
    /// <summary>
    /// The name of the site.
    /// </summary>
    [Required(ErrorMessage = "Site name is required")]
    [StringLength(50, ErrorMessage = "Name must be less than 100 characters")]
    public string Name { get; init; }

    /// <summary>
    /// The location of the site.
    /// </summary>
    [Required(ErrorMessage = "Location is required")]
    [StringLength(100, ErrorMessage = "Location must be less than 150 characters")]
    public string Location { get; init; }
}
