using Auftragsverwaltung.Infrastructure.Address;
using Auftragsverwaltung.Infrastructure.Article;
using Auftragsverwaltung.Infrastructure.ArticleGroup;
using Auftragsverwaltung.Infrastructure.Customer;
using Auftragsverwaltung.Infrastructure.Order;
using Auftragsverwaltung.Infrastructure.Position;
using Auftragsverwaltung.Infrastructure.Town;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

            modelBuilder.Entity<Domain.Customer.Customer>().HasData(
                new Domain.Customer.Customer
                {
                    CustomerId = 1,
                    CustomerNumber = "CU0001",
                    Firstname = "Mark",
                    Lastname = "Wahlberg",
                    Email = "mark.wahlberg@gmail.com",
                    Password = new byte[64]

                },
                new Domain.Customer.Customer
                {
                    CustomerId = 2,
                    CustomerNumber = "CU0002",
                    Firstname = "Jeff",
                    Lastname = "Bezos",
                    Email = "jeff.thebaldg@gmail.com",
                    Password = new byte[64]
                },
                new Domain.Customer.Customer
                {
                    CustomerId = 3,
                    CustomerNumber = "CU0003",
                    Firstname = "Hans-Rudolf",
                    Lastname = "Graf",
                    Email = "hr.graf77@gmail.com",
                    Website = "hans.ch",
                    Password = new byte[64]
                },
                new Domain.Address.Address
                {
                    AddressId = 1,
                    Street = "Jumbostrasse",
                    BuildingNr = "69",
                    ValidFrom = new System.DateTime(2020, 08, 30),
                    ValidUntil = new System.DateTime(2022, 10, 3),
                    TownId = 1,
                    CustomerId = 1,
                    Town = new Domain.Town.Town
                    {
                        TownId = 1,
                        Townname = "Heerbrugg",
                        ZipCode = "9435",
                    }
                },
                new Domain.Address.Address
                {
                    AddressId = 1,
                    Street = "Wumbostrasse",
                    BuildingNr = "420",
                    ValidFrom = new System.DateTime(2021, 04, 20),
                    ValidUntil = new System.DateTime(2042, 6, 9),
                    TownId = 1,
                    CustomerId = 1,
                    Town = new Domain.Town.Town
                    {
                        TownId = 1,
                        Townname = "Widnau",
                        ZipCode = "9443",
                    }
                },
                new Domain.Article.Article
                {
                    ArticleId = 1,
                    Description = "Zahnbürste",
                    Price = 2,
                    ArticleGroupId = 1,
                },
                new Domain.Article.Article
                {
                    ArticleId = 2,
                    Description = "Flaschenöffner",
                    Price = 25,
                    ArticleGroupId = 2,
                },
                new Domain.ArticleGroup.ArticleGroup
                {
                    ArticleGroupId = 1,
                    Name = "Körperpflege",

                },
                new Domain.ArticleGroup.ArticleGroup
                {
                    ArticleGroupId = 2,
                    Name = "Haushaltsprodukte",

                },
                new Domain.Order.Order
                {
                    OrderId = 1,
                    Date = new System.DateTime(2021, 8, 25),
                    CustomerId = 1,
                    
                },
                new Domain.Order.Order
                {
                    OrderId = 2,
                    Date = new System.DateTime(2021, 8, 20),
                    CustomerId = 2,
                    /*
                    Positions = new List<Position.Position>
                    {
                        new Position
                        {
                            Amount = 2,
                            Article = new Article()
                            {
                                ArticleId = 1,
                                ArticleGroup = new ArticleGroup()
                                {
                                    Name = "testarticle"
                                },
                                Description = "TestArticleDescription2",
                                Price = 22,
                            }
                        },
                        new Position
                        {
                            Amount = 2,
                            Article = new Article()
                            {
                                ArticleId = 2,
                                ArticleGroup = new ArticleGroup()
                                {
                                    Name = "testarticlegroup2"
                                },
                                Description = "testarticle2",
                                Price = 21,
                            }
                        },
                    },
                    */
                }
            );
        }
    }
}
