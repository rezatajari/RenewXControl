﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RenewXControl.Domain.Assets;
using RenewXControl.Domain.Users;

namespace RenewXControl.Infrastructure.Persistence.MyDbContext
{
    internal class RxcDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
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
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .HasMany(s => s.Sites)
                .WithOne(u => u.User)
                .HasForeignKey(s => s.UserId);

            // Site
            modelBuilder.Entity<Site>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Site>()
                .Property(s => s.Location)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Site>()
                .HasMany(a => a.Assets)
                .WithOne(a => a.Site)
                .HasForeignKey(a => a.SiteId);

            // Asset
            modelBuilder.Entity<Asset>()
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Asset>()
                .HasDiscriminator<string>("AssetType")
                .HasValue<Battery>("Battery")
                .HasValue<SolarPanel>("Solar")
                .HasValue<WindTurbine>("Wind");

            // Battery
            modelBuilder.Entity<Battery>()
                .Ignore(b => b.ChargeStateMessage);

            modelBuilder.Entity<Battery>()
                .Ignore(b => b.IsNeedToCharge);

            modelBuilder.Entity<Battery>()
                .Ignore(b => b.IsStartingCharge);

            // SolarPanel
            modelBuilder.Entity<SolarPanel>()
                .Ignore(s => s.PowerStatusMessage);

            // WindTurbine
            modelBuilder.Entity<WindTurbine>()
                .Ignore(w => w.PowerStatusMessage);

        }
    }
}
