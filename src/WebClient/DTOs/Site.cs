using System.ComponentModel.DataAnnotations;

namespace WebClient.DTOs;

public class Site
{
    [Required(ErrorMessage = "Site is required")]
    public Guid SiteId { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
}