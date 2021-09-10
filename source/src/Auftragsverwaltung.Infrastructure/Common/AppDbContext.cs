using Auftragsverwaltung.Infrastructure.Address;
using Auftragsverwaltung.Infrastructure.Article;
using Auftragsverwaltung.Infrastructure.ArticleGroup;
using Auftragsverwaltung.Infrastructure.Customer;
using Auftragsverwaltung.Infrastructure.Order;
using Auftragsverwaltung.Infrastructure.Position;
using Auftragsverwaltung.Infrastructure.Town;
using Microsoft.EntityFrameworkCore;
using System;
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
                    CustomerNumber = "CU00001",
                    Firstname = "Hans",
                    Lastname = "Müller",
                    Email = "hans@test.com",
                    Website = "www.hans.ch",
                    Password = new byte[64]

                },
                new Domain.Customer.Customer
                {
                    CustomerId = 2,
                    CustomerNumber = "CU00002",
                    Firstname = "Ida",
                    Lastname = "Muster",
                    Email = "ida@gmail.com",
                    Website = "www.ida.com",
                    Password = new byte[64]
                },
                new Domain.Customer.Customer
                {
                    CustomerId = 3,
                    CustomerNumber = "CU00003",
                    Firstname = "Vreni",
                    Lastname = "Müller",
                    Email = "vreni@test.com",
                    Website = "www.vreni.ch",
                    Password = new byte[64]
                });


            modelBuilder.Entity<Domain.Address.Address>().HasData(
                new Domain.Address.Address
                {
                    AddressId = 1,
                    Street = "Jumbostrasse",
                    BuildingNr = "69",
                    ValidFrom = new System.DateTime(2020, 08, 30),
                    ValidUntil = DateTime.MaxValue,
                    TownId = 1,
                    CustomerId = 1
                },
                new Domain.Address.Address
                {
                    AddressId = 2,
                    Street = "Wumbostrasse",
                    BuildingNr = "420",
                    ValidFrom = new System.DateTime(2019, 04, 20),
                    ValidUntil = new System.DateTime(2021, 01, 01),
                    TownId = 2,
                    CustomerId = 2
                },
                new Domain.Address.Address
                {
                    AddressId = 3,
                    Street = "Jumbostrasse",
                    BuildingNr = "69",
                    ValidFrom = new System.DateTime(2020, 08, 30),
                    ValidUntil = DateTime.MaxValue,
                    TownId = 1,
                    CustomerId = 3
                },
                new Domain.Address.Address
                {
                    AddressId = 4,
                    Street = "Dumbostrasse",
                    BuildingNr = "42",
                    ValidFrom = new System.DateTime(2021, 01, 02, 23, 59, 59),
                    ValidUntil = DateTime.MaxValue,
                    TownId = 2,
                    CustomerId = 2
                });


            modelBuilder.Entity<Domain.Town.Town>().HasData(
                new Domain.Town.Town
                {
                    TownId = 1,
                    Townname = "Heerbrugg",
                    ZipCode = "9435"
                },
                new Domain.Town.Town
                {
                    TownId = 2,
                    Townname = "Widnau",
                    ZipCode = "9443"
                });


            modelBuilder.Entity<Domain.Article.Article>().HasData(
                new Domain.Article.Article
                {
                    ArticleId = 1,
                    Description = "Zahnbürste",
                    Price = 2,
                    ArticleGroupId = 1
                },
                new Domain.Article.Article
                {
                    ArticleId = 2,
                    Description = "Flaschenöffner",
                    Price = 25,
                    ArticleGroupId = 2
                });

            modelBuilder.Entity<Domain.ArticleGroup.ArticleGroup>().HasData(
                new Domain.ArticleGroup.ArticleGroup
                {
                    ArticleGroupId = 1,
                    Name = "Pflegeprodukte"

                },
                new Domain.ArticleGroup.ArticleGroup
                {
                    ArticleGroupId = 2,
                    Name = "Haushaltsprodukte"

                },
                new Domain.ArticleGroup.ArticleGroup
                {
                    ArticleGroupId = 3,
                    Name = "Körperpflege",
                    ParentArticleGroupId = 1
                });

            modelBuilder.Entity<Domain.Order.Order>().HasData(
                new Domain.Order.Order
                {
                    OrderId = 1,
                    Date = new System.DateTime(2021, 8, 25),
                    CustomerId = 1
                },
                new Domain.Order.Order
                {
                    OrderId = 2,
                    Date = new System.DateTime(2021, 8, 20),
                    CustomerId = 2
                });

            modelBuilder.Entity<Domain.Position.Position>().HasData(
                new List<Domain.Position.Position>
                {
                    new Domain.Position.Position
                    {
                        ArticleId = 1,
                        OrderId = 1,
                        Amount = 2
                    },
                    new Domain.Position.Position
                    {
                        ArticleId = 2,
                        OrderId = 1,
                        Amount = 4
                    },
                    new Domain.Position.Position
                    {
                        ArticleId = 1,
                        OrderId = 2,
                        Amount = 12
                    },
                });

        }
    }
}
