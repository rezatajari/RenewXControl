using Domain.Entities.Assets;
using Domain.Entities.Site;
using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class RxcDbContext:IdentityDbContext<ApplicationUser,IdentityRole<Guid>,Guid>
{
    public DbSet<Site> Sites { get;set; }
    public DbSet<Asset> Assets { get; set; }


    public RxcDbContext(DbContextOptions<RxcDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User
        modelBuilder.Entity<ApplicationUser>()
            .HasMany(s => s.Sites)
            .WithOne()
            .HasForeignKey(s => s.UserId);

        // Site
        modelBuilder.Entity<Site>(site =>
        {
            site.Property(s => s.Name).IsRequired().HasMaxLength(50);
            site.Property(s => s.Location).IsRequired().HasMaxLength(100);
            site.HasMany(s => s.Assets).WithOne(s => s.Site).HasForeignKey(s => s.SiteId);
        });

        // Asset
        modelBuilder.Entity<Asset>(asset =>
        {
            asset.Property(a => a.Name).IsRequired().HasMaxLength(50);
            asset.HasDiscriminator<string>(name: "AssetType").HasValue<Battery>("Battery")
                .HasValue<SolarPanel>("Solar").HasValue<WindTurbine>("Wind");
        });

        // Battery
        modelBuilder.Entity<Battery>(battery =>
        {
            battery.Ignore(b => b.IsNeedToCharge);
            battery.Ignore(b => b.IsStartingChargeDischarge);
            battery.Ignore(b => b.TotalPower);
        });
    }
}