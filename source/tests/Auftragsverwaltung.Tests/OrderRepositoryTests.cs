using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Order;
using Auftragsverwaltung.Domain.Position;
using Auftragsverwaltung.Infrastructure.Article;
using Auftragsverwaltung.Infrastructure.Common;
using Auftragsverwaltung.Infrastructure.Customer;
using Auftragsverwaltung.Infrastructure.Order;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var order = InstanceHelper.GenerateOrderServiceTestData();
            var newOrder = order[0];

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

            var orderRepository = new OrderRepository(serviceScopeFactoryFake);

            //act
            var result = await orderRepository.Create(newOrder);

            //assert
            result.Entity.Customer.Firstname.Should().Be("Hans");
            result.Entity.Positions.Count.Should().Be(2);
            result.Should().BeOfType(typeof(ResponseDto<Order>));
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Create_WhenExceptionIsThrown_ReturnsCorrectResult()
        {
            //arrange
            var order = InstanceHelper.GenerateOrderServiceTestData();
            var newOrder = order[0];
            newOrder.OrderId = 1;

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

            var orderRepository = new OrderRepository(serviceScopeFactoryFake);

            //act
            var result = await orderRepository.Create(newOrder);

            //assert
            result.Should().BeOfType(typeof(ResponseDto<Order>));
            result.Flag.Should().BeFalse();
        }

        [Test]
        public async Task Create_WhenCustomerAndArticleExists_ReturnsCorrectResult()
        {
            //arrange
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

            var orderRepository = new OrderRepository(serviceScopeFactoryFake);
            var customerRepository = new CustomerRepository(serviceScopeFactoryFake);
            var articleRepository = new ArticleRepository(serviceScopeFactoryFake);

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
            result.Entity.Positions.First().Article.Description.Should().Be("Zahnbürste");
            result.Should().BeOfType(typeof(ResponseDto<Order>));
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
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

            var orderRepository = new OrderRepository(serviceScopeFactoryFake);

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

            var orderRepository = new OrderRepository(serviceScopeFactoryFake);

            //act
            var result = await orderRepository.GetAll();

            //assert
            var resultList = result.ToList();
            resultList.Should().BeOfType(typeof(List<Order>));
            resultList.Count().Should().Be(2);
        }

        [Test]
        public async Task Update_WhenOK_ReturnsCorrectResult()
        {
            //arrange
            var customerTestData = InstanceHelper.GenerateCustomerTestData();
            int orderId = 1;

            var newDate = new DateTime(2021, 08, 14);
            var newCustomer = customerTestData[1];
            newCustomer.CustomerId = 2;

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

            var orderRepository = new OrderRepository(serviceScopeFactoryFake);

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


        [Test]
        public async Task Update_WhenExceptionIsThrown_ReturnsCorrectResult()
        {
            //arrange
            var customerTestData = InstanceHelper.GenerateCustomerTestData();
            int orderId = 1;

            var newDate = new DateTime(2021, 08, 14);
            var newCustomer = customerTestData[1];
            newCustomer.CustomerId = 15;

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

            var orderRepository = new OrderRepository(serviceScopeFactoryFake);

            var entity = orderRepository.Get(orderId);
            var order = entity.Result;

            order.Customer = newCustomer;

            //act
            var result = await orderRepository.Update(order);

            //assert
            result.Should().BeOfType(typeof(ResponseDto<Order>));
            result.Flag.Should().BeFalse();
        }

        [Test]
        public async Task Delete_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int id = 1;
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

            var orderRepository = new OrderRepository(serviceScopeFactoryFake);

            //act
            var result = await orderRepository.Delete(id);

            //assert
            result.Should().BeOfType(typeof(ResponseDto<Order>));
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Delete_WhenExceptionIsThrown_ReturnsCorrectResult()
        {
            //arrange
            int id = 15;
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

            var orderRepository = new OrderRepository(serviceScopeFactoryFake);

            //act
            var result = await orderRepository.Delete(id);

            //assert
            result.Should().BeOfType(typeof(ResponseDto<Order>));
            result.Flag.Should().BeFalse();
        }

        [Test]
        public async Task Search_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            string searchString = "Hans";

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

            var orderRepository = new OrderRepository(serviceScopeFactoryFake);

            //act
            var result = await orderRepository.Search(searchString);

            //assert
            var resultList = result.ToList();
            resultList.Should().BeOfType(typeof(List<Order>));
            resultList.Count().Should().Be(1);
        }
    }
}
