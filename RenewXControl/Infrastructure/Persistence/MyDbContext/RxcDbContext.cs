using System;
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
        public DbSet<Battery> Batteries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Site> Sites { get;set; }

        public RxcDbContext(DbContextOptions<RxcDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
