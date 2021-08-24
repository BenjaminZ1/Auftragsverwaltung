using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auftragsverwaltung.Domain.Address;
using Auftragsverwaltung.Domain.Article;
using Auftragsverwaltung.Domain.ArticleGroup;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using Auftragsverwaltung.Domain.Order;
using Auftragsverwaltung.Domain.Position;
using Auftragsverwaltung.Domain.Town;
using Auftragsverwaltung.Infrastructure.Article;
using Auftragsverwaltung.Infrastructure.Common;
using Auftragsverwaltung.Infrastructure.Customer;
using Auftragsverwaltung.Infrastructure.Order;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Auftragsverwaltung.Tests
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
        public async Task Create_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            await InstanceHelper.AddDbTestArticles(_options);
            await InstanceHelper.AddDbTestCustomer(_options);
            var order = new Order()
            {
                Date = new DateTime(2020, 03, 03),
                Customer = new Customer()
                {
                    CustomerId = 1,
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
            };

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var orderRepository = new OrderRepository(dbContextFactoryFake);

            //act
            var result = await orderRepository.Create(order);

            //assert
            result.Entity.Customer.Firstname.Should().Be("Hans");
            result.Entity.Positions.Count.Should().Be(2);
            result.Should().BeOfType(typeof(ResponseDto<Order>));
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Create_WhenCustomerAndArticleExists_ReturnsCorrectResult()
        {
            //arrange
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));

            var serviceProviderFake = A.Fake<IServiceProvider>();
            A.CallTo(() => serviceProviderFake.GetService(typeof(AppDbContext)))
                .Returns(new AppDbContext(_options));

            var serviceScopeFake = A.Fake<IServiceScope>();
            A.CallTo(() => serviceScopeFake.ServiceProvider)
                .Returns(serviceProviderFake);

            var serviceScopeFactoryFake = A.Fake<IServiceScopeFactory>();
            A.CallTo(() => serviceScopeFactoryFake.CreateScope())
                .Returns(serviceScopeFake);

            A.CallTo(() => serviceProviderFake.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactoryFake);

            var customerRepo = new CustomerRepository(serviceScopeFactoryFake);
            var orderRepository = new OrderRepository(dbContextFactoryFake);
            var customerRepository = new CustomerRepository(serviceScopeFactoryFake);
            var articleRepository = new ArticleRepository(dbContextFactoryFake);

            await InstanceHelper.AddDbTestCustomer(_options);
            await InstanceHelper.AddDbTestArticles(_options);

            var order = new Order()
            {
                Date = new DateTime(2020, 03, 03),
                Customer = await customerRepository.Get(1),
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
            result.Entity.Positions.First().Article.Description.Should().Be("testArticleDescription");
            result.Should().BeOfType(typeof(ResponseDto<Order>));
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            await InstanceHelper.AddDbTestCustomer(_options);
            await InstanceHelper.AddDbTestArticles(_options);
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

        [Test]
        public async Task GetAll_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            await InstanceHelper.AddDbTestCustomer(_options);
            await InstanceHelper.AddDbTestArticles(_options);

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var orderRepository = new OrderRepository(dbContextFactoryFake);

            await InstanceHelper.AddDbTestOrder(_options);

            //act
            var result = await orderRepository.GetAll();

            //assert
            var resultList = result.ToList();
            resultList.Should().BeOfType(typeof(List<Order>));
            resultList.Count().Should().Be(1);
        }

        [Test]
        public async Task Update_WhenOK_ReturnsCorrectResult()
        {
            //arrange
            await InstanceHelper.AddDbTestCustomers(_options);
            await InstanceHelper.AddDbTestArticles(_options);
            await InstanceHelper.AddDbTestOrder(_options);

            var customerTestData = InstanceHelper.GetCustomerTestData();
            int orderId = 1;

            var newDate = new DateTime(2021, 08, 14);
            var newCustomer = customerTestData[1];
            newCustomer.CustomerId = 2;

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var orderRepository = new OrderRepository(dbContextFactoryFake);

            var entity = orderRepository.Get(orderId);
            var order = entity.Result;

            var modifiedPositions = order.Positions.ToList();
            modifiedPositions[0].Amount = 13;

            order.Date = newDate;
            order.Customer = newCustomer;
            order.Positions = modifiedPositions;


            //act
            var result = await orderRepository.Update(order);

            //assert
            result.Entity.Date.Should().Be(newDate);
            result.Entity.Customer.CustomerId.Should().Be(newCustomer.CustomerId);
            result.Entity.Positions.Should().BeEquivalentTo(modifiedPositions);
            result.Flag.Should().BeTrue();
        }
    }
}
