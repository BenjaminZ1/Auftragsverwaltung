using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain;
using Auftragsverwaltung.Infrastructure.Article;
using Auftragsverwaltung.Infrastructure.Common;
using Auftragsverwaltung.Infrastructure.Customer;
using Auftragsverwaltung.Infrastructure.Order;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Auftragsverwaltung.Repository.Tests
{
    public class OrderRepositoryTests
    {
        private DbContextOptions<AppDbContext> _options;

        [OneTimeSetUp]
        public void BuidDbContext()
        {
            _options = InstanceHelper.AppDbContext_BuildDbContext();
        }

        [SetUp]
        public void ResetDb()
        {
            InstanceHelper.ResetDb(_options);
        }

        private async Task AddDbTestEntries()
        {
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
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

        [Test]
        public async Task Create_WhenAllNew_ReturnsCorrectResult()
        {
            //arrange
            var order = new Order()
            {
                Date = new DateTime(2020,03,03),
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
                    new Position()
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
                },
            };

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var orderRepository = new OrderRepository(dbContextFactoryFake);

            //act
            var result = await orderRepository.Create(order);

            //assert
            result.Entity.Customer.Firstname.Should().Be("Hans");
            result.Entity.Positions.Count.Should().Be(1);
            result.Should().BeOfType(typeof(ResponseDto<Order>));
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Create_WhenCustomerAndArticleExists_ReturnsCorrectResult()
        {
            await AddDbTestEntries();

            //arrange
            var order = new Order()
            {
                Date = new DateTime(2020, 03, 03),
                CustomerId = 1,
                Positions = new List<Position>
                {
                    new Position()
                    {
                        Amount = 2,
                        ArticleId = 1
                    },
                },
            };


            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var orderRepository = new OrderRepository(dbContextFactoryFake);

            //act
            var result = await orderRepository.Create(order);

            //assert
            result.Entity.Customer.Firstname.Should().Be("Hans");
            result.Should().BeOfType(typeof(ResponseDto<Order>));
            result.Flag.Should().BeTrue();
        }


    }
}
