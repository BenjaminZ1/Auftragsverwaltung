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
            await customerRepo.Create(new Customer()
            {
                Address = new Address()
                {
                    Street = "Hauptstrasse",
                    BuildingNr = "44",
                    Town = new Town()
                    {
                        Townname = "St. Gallen",
                        ZipCode = "9001"
                    }
                },
                Firstname = "Ida",
                Lastname = "Muster",
                Email = "ida@gmail.com",
                Website = "www.ida.com",
                Password = new byte[64]
            });
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
                Firstname = "Vreni",
                Lastname = "Müller",
                Email = "vreni@test.com",
                Website = "www.vreni.ch",
                Password = new byte[64]
            });
        }

        private async Task AddSingleDbTestEntry()
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
                Lastname = "Müller",
                Email = "test@test.com",
                Website = "www.test.ch",
                Password = new byte[64]
            };
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var customerRepo = new CustomerRepository(dbContextFactoryFake);

            //act
            var result = await customerRepo.Create(customer);

            //assert
            result.Should().BeOfType(typeof(ResponseDto<Customer>));
            result.Entity.Address.AddressId.IsSameOrEqualTo(1);
            result.Entity.Address.Town.TownId.IsSameOrEqualTo(1);
            result.Entity.AddressId.IsSameOrEqualTo(result.Entity.Address.AddressId);
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Create_WhenAddressAndTownAlreadyExist_ReturnsCorrectResult()
        {
            //arrange
            await AddDbTestEntries();
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
                Lastname = "Müller",
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
            result.Entity.Address.AddressId.IsSameOrEqualTo(expectedId);
            result.Entity.Address.Town.TownId.IsSameOrEqualTo(expectedId);
            result.Entity.AddressId.IsSameOrEqualTo(result.Entity.Address.AddressId);
            result.Flag.Should().BeTrue();
        }

        [Test]
        public async Task Get_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            await AddDbTestEntries();

            int id = 1;
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var customerRepo = new CustomerRepository(dbContextFactoryFake);

            //act
            var result = await customerRepo.Get(id);

            //assert
            result.Should().BeOfType(typeof(Customer));
            result.CustomerId.Should().Be(id);
            result.Address.Should().NotBeNull();
        }

        [Test]
        public async Task GetAll_WhenOk_ReturnsCorrectResult()
        {
            //arrange
            await AddDbTestEntries();

            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var customerRepo = new CustomerRepository(dbContextFactoryFake);

            //act
            var result = await customerRepo.GetAll();

            //assert
            result.Should().BeOfType(typeof(List<Customer>));
            result.Count().Should().Be(3);
        }

        [Test]
        public async Task Delete_WhenAddressIsNotInMultipleRelations_ReturnsCorrectResult()
        {
            //arrange
            await AddSingleDbTestEntry();
            int id = 1;
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var customerRepo = new CustomerRepository(dbContextFactoryFake);

            //act
            var result = await customerRepo.Delete(id);

            //assert
            result.Flag.Should().BeTrue();
            result.NumberOfRows.Should().Be(3);
        }

        [Test]
        public async Task Delete_WhenAddressIsInMultipleRelations_ReturnsCorrectResult()
        {
            //arrange
            await AddDbTestEntries();
            int id = 1;
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var customerRepo = new CustomerRepository(dbContextFactoryFake);

            //act
            var result = await customerRepo.Delete(id);

            //assert
            result.Flag.Should().BeTrue();
            result.NumberOfRows.Should().Be(1);
        }

        [Test]
        public async Task Update_WhenOK_ReturnsCorrectResult()
        {
            //arrange
            await AddDbTestEntries();
            int id = 1;
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(_options));
            var customerRepo = new CustomerRepository(dbContextFactoryFake);

            var entity = customerRepo.Get(id);
            var customer = entity.Result;
            customer.Firstname = "Hans-Rudolf";

            //act
            var result = await customerRepo.Update(id, customer);

            //assert
            result.Entity.Firstname.Should().BeEquivalentTo(customer.Firstname);
            result.Flag.Should().BeTrue();
        }
    }
}