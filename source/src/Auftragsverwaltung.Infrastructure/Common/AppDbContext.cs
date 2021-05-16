using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Infrastructure.Address;
using Auftragsverwaltung.Infrastructure.Customer;
using Microsoft.EntityFrameworkCore;

namespace Auftragsverwaltung.Infrastructure.Common
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Domain.Customer> Customers { get; set; }
        public DbSet<Domain.Address> Addresses { get; set; }
        public DbSet<Domain.Town> Towns { get; set; }
        public DbSet<Domain.Order> Orders { get; set; }
        public DbSet<Domain.Position> Positions { get; set; }
        public DbSet<Domain.Article> Articles { get; set; }
        public DbSet<Domain.ArticleGroup> ArticleGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
        }
    }
}
