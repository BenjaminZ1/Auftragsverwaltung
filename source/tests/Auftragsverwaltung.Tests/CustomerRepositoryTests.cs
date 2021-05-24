using System.Threading.Tasks;
using Auftragsverwaltung.Domain;
using Auftragsverwaltung.Infrastructure.Common;
using Auftragsverwaltung.Infrastructure.Customer;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Auftragsverwaltung.Tests
{
    [TestFixture]
    public class CarRepositoryTests
    {
        private DbContextOptions<AppDbContext> _options;

        [OneTimeSetUp]
        public void CarDbContext_BuildDbContext()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("testDb")
                .EnableSensitiveDataLogging()
                .Options;

            //_options = new DbContextOptionsBuilder<AppDbContext>()
            //    .UseSqlServer("Data Source=.\\ZBW; Database=Auftragsverwaltung; Trusted_Connection=True")
            //    .EnableSensitiveDataLogging()
            //    .Options;
        }

        [SetUp]
        public void ResetDb()
        {
            using var context = new AppDbContext(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        private void AddDbTestEntries()
        {
            using var context = new AppDbContext(_options);
            context.Customers.Add(new Customer()
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
                Email = "test@test.com",
                Website = "www.test.ch",
                Password = new byte[64]
            });
            context.Customers.Add(new Customer()
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

            context.SaveChanges();
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
            result.Should().BeOfType(typeof(Customer));
            result.Address.AddressId.IsSameOrEqualTo(1);
            result.Address.Town.TownId.IsSameOrEqualTo(1);
            result.AddressId.IsSameOrEqualTo(result.Address.AddressId);
        }

        [Test]
        public async Task Create_WhenAddressAndTownAlreadyExist_ReturnsCorrectResult()
        {
            //arrange
            AddDbTestEntries();
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
            result.Should().BeOfType(typeof(Customer));
            result.Address.AddressId.IsSameOrEqualTo(1);
            result.Address.Town.TownId.IsSameOrEqualTo(1);
            result.AddressId.IsSameOrEqualTo(result.Address.AddressId);
        }
    }
}