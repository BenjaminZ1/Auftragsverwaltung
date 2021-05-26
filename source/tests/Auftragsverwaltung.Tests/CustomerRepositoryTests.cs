using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain;
using Auftragsverwaltung.Infrastructure.Common;
using Auftragsverwaltung.Infrastructure.Customer;
using Auftragsverwaltung.Repository.Tests;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Auftragsverwaltung.Repository.Tests
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
            var customer = new Customer()
            {
                Address = new Address()
                {
                    Street = "Teststrasse",
                    BuildingNr = "2",
                    Town = new Town()
                    {
                        Townname = "Teststadt",
                        ZipCode = "9999"
                    }
                },
                Firstname = "Hans",
                Lastname = "M�ller",
                Email = "test@test.com",
                Website = "www.test.ch",
                Password = new byte[64]
            };
            int expectedId = 1;

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var customerRepo = new CustomerRepository(dbContextFactoryFake);

            //act
            var result = await customerRepo.Create(customer);

            //assert
            result.Should().BeOfType(typeof(ResponseDto<Customer>));
            result.Entity.Address.AddressId.Should().Be(expectedId);
            result.Entity.Address.Town.TownId.Should().Be(expectedId);
            result.Entity.AddressId.Should().Be(result.Entity.Address.AddressId);
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Create_WhenAddressAndTownAlreadyExist_ReturnsCorrectResult()
        {
            //arrange
            await InstanceHelper.AddDbTestCustomers(_options);
            var customer = new Customer()
            {
                Address = new Address()
                {
                    Street = "Teststrasse",
                    BuildingNr = "2",
                    Town = new Town()
                    {
                        Townname = "Teststadt",
                        ZipCode = "9999"
                    }
                },
                Firstname = "Hans",
                Lastname = "M�ller",
                Email = "test@test.com",
                Website = "www.test.ch",
                Password = new byte[64]
            };
            int expectedId = 1;

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var customerRepo = new CustomerRepository(dbContextFactoryFake);

            //act
            var result = await customerRepo.Create(customer);

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

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var customerRepo = new CustomerRepository(dbContextFactoryFake);

            //act
            var result = await customerRepo.Get(customerId);

            //assert
            result.Should().BeOfType(typeof(Customer));
            result.CustomerId.Should().Be(customerId);
            result.Address.Should().NotBeNull();
        }

        [Test]
        public async Task GetAll_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            await InstanceHelper.AddDbTestCustomers(_options);

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var customerRepo = new CustomerRepository(dbContextFactoryFake);

            //act
            var result = await customerRepo.GetAll();

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

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var customerRepo = new CustomerRepository(dbContextFactoryFake);

            //act
            var result = await customerRepo.Delete(customerId);

            //assert
            result.Flag.Should().BeTrue();
            result.NumberOfRows.Should().Be(expectedRows);
        }

        [Test]
        public async Task Delete_WhenAddressIsInMultipleRelations_ReturnsCorrectResult()
        {
            //arrange
            await InstanceHelper.AddDbTestCustomers(_options);
            int customerId = 1;
            int expectedRows = 1;

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var customerRepo = new CustomerRepository(dbContextFactoryFake);

            //act
            var result = await customerRepo.Delete(customerId);

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
            var customerRepo = new CustomerRepository(dbContextFactoryFake);

            var entity = customerRepo.Get(customerId);
            var customer = entity.Result;
            customer.Firstname = newFirstname;

            //act
            var result = await customerRepo.Update(customerId, customer);

            //assert
            result.Entity.Firstname.Should().BeEquivalentTo(newFirstname);
            result.Flag.Should().BeTrue();
        }
    }
}