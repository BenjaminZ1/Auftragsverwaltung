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
            //arrange
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var orderRepository = new OrderRepository(dbContextFactoryFake);
            var customerRepo = new CustomerRepository(dbContextFactoryFake);
            var articleRepository = new ArticleRepository(dbContextFactoryFake);

            await InstanceHelper.AddDbTestCustomer(_options);
            await InstanceHelper.AddDbTestArticle(_options);

            var order = new Order()
            {
                Date = new DateTime(2020, 03, 03),
                Customer = await customerRepo.Get(1),
                    Positions = new List<Position>
                    {
                        new Position()
                        {
                            Amount = 2,
                            Article = await articleRepository.Get(1)
                        },
                    },
            };

            //act
            var result = await orderRepository.Create(order);

            //assert
            result.Entity.Customer.Firstname.Should().Be("Hans");
            result.Entity.Positions.First().Article.Description.Should().Be("TestArticleDescription2");
            result.Should().BeOfType(typeof(ResponseDto<Order>));
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var orderRepository = new OrderRepository(dbContextFactoryFake);

            await InstanceHelper.AddDbTestOrder(_options);

            //act
            var result = await orderRepository.Get(1);

            //assert
            result.Should().BeOfType(typeof(Order));
            result.CustomerId.Should().Be(1);
            result.Positions.Count.Should().Be(2);
        }
    }
}
