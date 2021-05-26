using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auftragsverwaltung.Domain;
using Auftragsverwaltung.Infrastructure.Article;
using Auftragsverwaltung.Infrastructure.Common;
using Auftragsverwaltung.Infrastructure.Customer;
using Auftragsverwaltung.Infrastructure.Order;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Auftragsverwaltung.Repository.Tests
{
    public static class InstanceHelper
    {
        
        public static DbContextOptions<AppDbContext> AppDbContext_BuildDbContext()
        {
            //return new DbContextOptionsBuilder<AppDbContext>()
            //    .UseInMemoryDatabase("testDb")
            //    .EnableSensitiveDataLogging()
            //    .Options;

            return new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Data Source=.\\ZBW; Database=Auftragsverwaltung; Trusted_Connection=True")
                .EnableSensitiveDataLogging()
                .Options;
        }

        
        public static void ResetDb(DbContextOptions<AppDbContext> options)
        {
            using var context = new AppDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public static async Task AddDbTestCustomer(DbContextOptions<AppDbContext> options)
        {
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(options));
            var customerRepo = new CustomerRepository(dbContextFactoryFake);

            await customerRepo.Create(new Customer()
            {
                Address = new Address()
                {
                    Street = "Teststrasse",
                    BuildingNr = "2",
                    Town = new Town()
                    {
                        Townname = "Herisau",
                        ZipCode = "9100"
                    }
                },
                Firstname = "Hans",
                Lastname = "Müller",
                Email = "hans@test.com",
                Website = "www.hans.ch",
                Password = new byte[64]
            });
        }

        public static async Task AddDbTestArticle(DbContextOptions<AppDbContext> options)
        {
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(options));
            
            var articleRepository = new ArticleRepository(dbContextFactoryFake);

            await articleRepository.Create(new Article()
            {
                ArticleGroup = new ArticleGroup()
                {
                    Name = "testarticle"
                },
                Description = "TestArticleDescription2",
                Price = 22,
            });
        }

        public static async Task AddDbTestOrder(DbContextOptions<AppDbContext> options)
        {
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(options));

            var orderRepository = new OrderRepository(dbContextFactoryFake);

            await orderRepository.Create(new Order()
            {
                Date = new DateTime(2020, 03, 03),
                Customer = new Customer()
                {
                    Address = new Address()
                    {
                        Street = "Teststrasse",
                        BuildingNr = "2",
                        Town = new Town()
                        {
                            Townname = "Herisau",
                            ZipCode = "9100"
                        }
                    },
                    Firstname = "Hans",
                    Lastname = "Müller",
                    Email = "hans@test.com",
                    Website = "www.hans.ch",
                    Password = new byte[64]
                },
                Positions = new List<Position>
                {
                    new Position
                    {
                        Amount = 2,
                        Article = new Article()
                        {
                            ArticleGroup = new ArticleGroup()
                            {
                                Name = "testarticlegroup"
                            },
                            Description = "testarticle",
                            Price = 22,
                        }
                    },
                    new Position
                    {
                        Amount = 2,
                        Article = new Article()
                        {
                            ArticleGroup = new ArticleGroup()
                            {
                                Name = "testarticlegroup2"
                            },
                            Description = "testarticle2",
                            Price = 21,
                        }
                    },
                },
            });
        }
    }
}
