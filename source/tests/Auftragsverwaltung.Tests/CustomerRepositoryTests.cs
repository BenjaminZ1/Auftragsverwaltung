using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auftragsverwaltung.Domain.Address;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using Auftragsverwaltung.Domain.Town;
using Auftragsverwaltung.Infrastructure.Common;
using Auftragsverwaltung.Infrastructure.Customer;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

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

            int expectedId = 1;

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var customerRepository = new CustomerRepository(dbContextFactoryFake);

            //act
            var result = await customerRepository.Create(customer);

            //assert
            result.Should().BeOfType(typeof(ResponseDto<Customer>));
            result.Entity.Address.AddressId.Should().Be(expectedId);
            result.Entity.Address.Town.TownId.Should().Be(expectedId);
            result.Entity.AddressId.Should().Be(result.Entity.Address.AddressId);
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            await InstanceHelper.AddDbTestCustomers(_options);
            int customerId = 1;

            IServiceProvider serviceProvider = InstanceHelper.CreateServiceProvider();
            var serviceScopeFactoryFake = A.Fake<IServiceScopeFactory>();
            A.CallTo(() => serviceScopeFactoryFake.CreateScope()).Returns(serviceProvider.CreateScope());
            var customerRepository = new CustomerRepository(serviceScopeFactoryFake);

            //act
            var result = await customerRepository.Get(customerId);

            //assert
            result.Should().BeOfType(typeof(Customer));
            result.CustomerId.Should().Be(customerId);
            result.Address.Should().NotBeNull();
        }

        [Test]
        public async Task Get_WhenCustomerNotExists_ReturnsCorrectResult()
        {
            //arrange
            await InstanceHelper.AddDbTestCustomers(_options);
            int notExistingCustomerId = 4;

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var customerRepository = new CustomerRepository(dbContextFactoryFake);

            //act
            var result = await customerRepository.Get(notExistingCustomerId);

            //assert
            result.Should().BeNull();
        }

        [Test]
        public async Task GetAll_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            await InstanceHelper.AddDbTestCustomers(_options);

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var customerRepository = new CustomerRepository(dbContextFactoryFake);

            //act
            var result = await customerRepository.GetAll();

            //assert
            var resultList = result.ToList();
            resultList.Should().BeOfType(typeof(List<Customer>));
            resultList.Count().Should().Be(3);
        }

        [Test]
        public async Task GetAll_WhenNoCustomerExists_ReturnsCorrectResult()
        {
            //arrange
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var customerRepository = new CustomerRepository(dbContextFactoryFake);

            //act
            var result = await customerRepository.GetAll();

            //assert
            var resultList = result.ToList();
            resultList.Should().BeEmpty();
        }

        [Test]
        public async Task Delete_WhenAddressIsNotInMultipleRelations_ReturnsCorrectResult()
        {
            //arrange
            await InstanceHelper.AddDbTestCustomer(_options);
            int customerId = 1;
            int expectedRows = 3;

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var customerRepository = new CustomerRepository(dbContextFactoryFake);

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
            await InstanceHelper.AddDbTestCustomers(_options);
            int customerId = 1;
            string newFirstname = "Hans-Rudolf";

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var customerRepository = new CustomerRepository(dbContextFactoryFake);

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