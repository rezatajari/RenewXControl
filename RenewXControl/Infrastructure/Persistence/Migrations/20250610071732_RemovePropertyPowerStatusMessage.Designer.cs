﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RenewXControl.Infrastructure.Persistence;


#nullable disable

namespace RenewXControl.Migrations
{
    [DbContext(typeof(RxcDbContext))]
    [Migration("20250610071732_RemovePropertyPowerStatusMessage")]
    partial class RemovePropertyPowerStatusMessage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RenewXControl.Domain.Assets.Asset", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AssetType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("SiteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("Assets");

                    b.HasDiscriminator<string>("AssetType").HasValue("Asset");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("RenewXControl.Domain.Users.Site", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("RenewXControl.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RenewXControl.Domain.Assets.Battery", b =>
                {
                    b.HasBaseType("RenewXControl.Domain.Assets.Asset");

                    b.Property<double>("SetPoint")
                        .HasColumnType("float");

                    b.Property<double>("StateCharge")
                        .HasColumnType("float");

                    b.HasDiscriminator().HasValue("Battery");
                });

            modelBuilder.Entity("RenewXControl.Domain.Assets.SolarPanel", b =>
                {
                    b.HasBaseType("RenewXControl.Domain.Assets.Asset");

                    b.Property<double>("ActivePower")
                        .HasColumnType("float");

                    b.Property<double>("Irradiance")
                        .HasColumnType("float");

                    b.Property<double>("SetPoint")
                        .HasColumnType("float");

                    b.ToTable("Assets", t =>
                        {
                            t.Property("SetPoint")
                                .HasColumnName("SolarPanel_SetPoint");
                        });

                    b.HasDiscriminator().HasValue("Solar");
                });

            modelBuilder.Entity("RenewXControl.Domain.Assets.WindTurbine", b =>
                {
                    b.HasBaseType("RenewXControl.Domain.Assets.Asset");

                    b.Property<double>("ActivePower")
                        .HasColumnType("float");

                    b.Property<double>("SetPoint")
                        .HasColumnType("float");

                    b.Property<double>("WindSpeed")
                        .HasColumnType("float");

                    b.ToTable("Assets", t =>
                        {
                            t.Property("ActivePower")
                                .HasColumnName("WindTurbine_ActivePower");

                            t.Property("SetPoint")
                                .HasColumnName("WindTurbine_SetPoint");
                        });

                    b.HasDiscriminator().HasValue("Wind");
                });

            modelBuilder.Entity("RenewXControl.Domain.Assets.Asset", b =>
                {
                    b.HasOne("RenewXControl.Domain.Users.Site", "Site")
                        .WithMany("Assets")
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Site");
                });

            modelBuilder.Entity("RenewXControl.Domain.Users.Site", b =>
                {
                    b.HasOne("RenewXControl.Domain.Users.User", "User")
                        .WithMany("Sites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RenewXControl.Domain.Users.Site", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("RenewXControl.Domain.Users.User", b =>
                {
                    b.Navigation("Sites");
                });
#pragma warning restore 612, 618
        }
    }
}
