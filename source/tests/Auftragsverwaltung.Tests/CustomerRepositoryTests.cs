using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using Auftragsverwaltung.Infrastructure.Common;
using Auftragsverwaltung.Infrastructure.Customer;
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
    [TestFixture]
    public class CustomerRepositoryTests
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
        public async Task Create_WhenNew_ReturnsCorrectResult()
        {
            //arrange
            var customerTestData = InstanceHelper.GetCustomerTestData();
            var customer = customerTestData[0];

            int expectedAddressId = 4;
            int expectedTownId = 3;

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

            var customerRepository = new CustomerRepository(serviceScopeFactoryFake);

            //act
            var result = await customerRepository.Create(customer);

            //assert
            result.Should().BeOfType(typeof(ResponseDto<Customer>));
            result.Entity.Addresses.First().AddressId.Should().Be(expectedAddressId);
            result.Entity.Addresses.First().Town.TownId.Should().Be(expectedTownId);
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            int customerId = 1;

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
            var customerRepository = new CustomerRepository(serviceScopeFactoryFake);

            //act
            var result = await customerRepository.Get(customerId);

            //assert
            result.Should().BeOfType(typeof(Customer));
            result.CustomerId.Should().Be(customerId);
            result.Addresses.First().Should().NotBeNull();
        }

        [Test]
        public async Task Get_WhenCustomerNotExists_ReturnsCorrectResult()
        {
            //arrange
            int notExistingCustomerId = 4;

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
            var customerRepository = new CustomerRepository(serviceScopeFactoryFake);

            //act
            var result = await customerRepository.Get(notExistingCustomerId);

            //assert
            result.Should().BeNull();
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
            var customerRepository = new CustomerRepository(serviceScopeFactoryFake);

            //act
            var result = await customerRepository.GetAll();

            //assert
            var resultList = result.ToList();
            resultList.Should().BeOfType(typeof(List<Customer>));
            resultList.Count().Should().Be(3);
        }

        [Test]
        public async Task Delete_WhenAddressIsNotInMultipleRelations_ReturnsCorrectResult()
        {
            //arrange
            await InstanceHelper.AddDbTestCustomer(_options);
            int customerId = 1;
            int expectedRows = 3;

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
            var customerRepository = new CustomerRepository(serviceScopeFactoryFake);

            //act
            var result = await customerRepository.Delete(customerId);

            //assert
            result.Flag.Should().BeTrue();
            result.NumberOfRows.Should().Be(expectedRows);
        }

        [Test]
        public async Task Update_WhenOK_ReturnsCorrectResult()
        {
            //arrange
            int customerId = 1;
            string newFirstname = "Hans-Rudolf";

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
            var customerRepository = new CustomerRepository(serviceScopeFactoryFake);

            var entity = customerRepository.Get(customerId);
            var customer = entity.Result;
            customer.Firstname = newFirstname;

            //act
            var result = await customerRepository.Update(customer);

            //assert
            result.Entity.Firstname.Should().BeEquivalentTo(newFirstname);
            result.Flag.Should().BeTrue();
        }
    }
}