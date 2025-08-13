using Domain.Entities.Assets;

namespace Application.DTOs;

public class UserMonitoringInfo
{
    public string? Username { get; set; }
    public string? SiteName { get; set; }
    public string? SiteLocation { get; set; }

    // Domain entities for behavior
    public SolarPanel? SolarPanel { get; set; }
    public WindTurbine? WindTurbine { get; set; }
    public Battery? Battery { get; set; }
}