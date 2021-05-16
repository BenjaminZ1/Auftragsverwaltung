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
                        Townname = "Teststadt",
                        ZipCode = "9999"
                    }
                },
                Firstname = "Hans",
                Lastname = "Müller",
                Email = "test@test.com",
                Website = "www.test.ch",
                Password = new byte[64]
            });

            context.SaveChanges();
        }

        class TestAppDbContextFactory : AppDbContextFactory
        {
            public new AppDbContext CreateDbContext(string[] args = null)
            {
                //var configuration = new ConfigurationBuilder()
                //    .SetBasePath(Directory.GetCurrentDirectory())
                //    .AddJsonFile("appsettings.json", true)
                //    .AddEnvironmentVariables()
                //    .Build();

                var builder = new DbContextOptionsBuilder<AppDbContext>();
                var connectionString = "Data Source=.\\ZBW; Database=Auftragsverwaltung; Trusted_Connection=True";

                builder.UseInMemoryDatabase("testDb")
                    .EnableSensitiveDataLogging();

                return new AppDbContext(builder.Options);
            }
        }

        [Test]
        public async Task Create_WhenNew_ReturnsCorrectResult()
        {
            //arrange
            var appDbContextFactoryFake = new TestAppDbContextFactory();
            var customerRepo = new CustomerRepository(appDbContextFactoryFake);
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

            //act
            var result = await customerRepo.Create(customer);

            //assert
            result.Firstname.IsSameOrEqualTo(customer.Firstname);
        }
    }
}