﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RenewXControl.Domain.Assets;
using RenewXControl.Domain.Site;
using RenewXControl.Domain.User;

namespace RenewXControl.Infrastructure.Persistence
{
    public class RxcDbContext:IdentityDbContext<User>
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
            modelBuilder.Entity<User>()
                .HasMany(s => s.Sites)
                .WithOne(u => u.User)
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
}
