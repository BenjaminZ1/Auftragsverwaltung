using Auftragsverwaltung.Infrastructure.Address;
using Auftragsverwaltung.Infrastructure.Article;
using Auftragsverwaltung.Infrastructure.ArticleGroup;
using Auftragsverwaltung.Infrastructure.Customer;
using Auftragsverwaltung.Infrastructure.Order;
using Auftragsverwaltung.Infrastructure.Position;
using Auftragsverwaltung.Infrastructure.Town;
using Microsoft.EntityFrameworkCore;

namespace Auftragsverwaltung.Infrastructure.Common
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Domain.Customer.Customer> Customers { get; set; }
        public DbSet<Domain.Address.Address> Addresses { get; set; }
        public DbSet<Domain.Town.Town> Towns { get; set; }
        public DbSet<Domain.Order.Order> Orders { get; set; }
        public DbSet<Domain.Position.Position> Positions { get; set; }
        public DbSet<Domain.Article.Article> Articles { get; set; }
        public DbSet<Domain.ArticleGroup.ArticleGroup> ArticleGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new TownConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleGroupConfiguration());
        }
    }
}
