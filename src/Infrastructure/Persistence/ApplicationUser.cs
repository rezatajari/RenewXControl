using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? ProfileImage { get; set; }
    public DateTime CreateTime { get; set; }

    public ICollection<Site> Sites { get; set; } = new List<Site>();
}