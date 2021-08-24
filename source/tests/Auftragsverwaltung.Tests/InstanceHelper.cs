using System;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Mapper;
using Auftragsverwaltung.Application.Validators;
using Auftragsverwaltung.Domain.Address;
using Auftragsverwaltung.Domain.Article;
using Auftragsverwaltung.Domain.ArticleGroup;
using Auftragsverwaltung.Domain.Customer;
using Auftragsverwaltung.Domain.Order;
using Auftragsverwaltung.Domain.Position;
using Auftragsverwaltung.Domain.Town;
using Auftragsverwaltung.Infrastructure.Article;
using Auftragsverwaltung.Infrastructure.ArticleGroup;
using Auftragsverwaltung.Infrastructure.Common;
using Auftragsverwaltung.Infrastructure.Customer;
using Auftragsverwaltung.Infrastructure.Order;
using AutoMapper;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;

namespace Auftragsverwaltung.Tests
{
    public static class InstanceHelper
    {

        public static DbContextOptions<AppDbContext> AppDbContext_BuildDbContext()
        {
            return new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("testDb")
                .EnableSensitiveDataLogging()
                .Options;

            //return new DbContextOptionsBuilder<AppDbContext>()
            //    .UseSqlServer("Data Source=.\\ZBW; Database=Auftragsverwaltung; Trusted_Connection=True")
            //    .EnableSensitiveDataLogging()
            //    .Options;
        }


        public static void ResetDb(DbContextOptions<AppDbContext> options)
        {
            using var context = new AppDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public static async Task AddDbTestCustomer(DbContextOptions<AppDbContext> options)
        {
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(options));
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
                CustomerNumber = "CU00001",
                Firstname = "Hans",
                Lastname = "Müller",
                Email = "hans@test.com",
                Website = "www.hans.ch",
                Password = new byte[64]
            });
        }

        public static async Task AddDbTestCustomers(DbContextOptions<AppDbContext> options)
        {
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(options));
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
                CustomerNumber = "CU00001",
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
                CustomerNumber = "CU00002",
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
                CustomerNumber = "CU00003",
                Firstname = "Vreni",
                Lastname = "Müller",
                Email = "vreni@test.com",
                Website = "www.vreni.ch",
                Password = new byte[64]
            });
        }

        public static async Task AddDbTestArticles(DbContextOptions<AppDbContext> options)
        {
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(options));

            var articleRepository = new ArticleRepository(dbContextFactoryFake);

            await articleRepository.Create(new Article()
            {
                ArticleGroup = new ArticleGroup()
                {
                    Name = "testArticleGroup"
                },
                Description = "testArticleDescription",
                Price = 22,
            });

            await articleRepository.Create(new Article()
            {
                ArticleGroup = new ArticleGroup()
                {
                    Name = "testArticleGroup2"
                },
                Description = "testArticleDescription2",
                Price = 21,
            });
        }

        public static async Task AddDbTestArticleGroup(DbContextOptions<AppDbContext> options)
        {
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(options));

            var articleGroupRepository = new ArticleGroupRepository(dbContextFactoryFake);

            await articleGroupRepository.Create(new ArticleGroup()
            {
                Name = "testArticleGroup"
            });
            await articleGroupRepository.Create(new ArticleGroup()
            {
                Name = "testArticleGroup2",
                ParentArticleGroup = await articleGroupRepository.Get(1)
            });
        }

        public static async Task AddDbTestOrder(DbContextOptions<AppDbContext> options)
        {
            var dbContextFactoryFake = A.Fake<AppDbContextFactory>();
            A.CallTo(() => dbContextFactoryFake.CreateDbContext(null)).Returns(new AppDbContext(options));

            var orderRepository = new OrderRepository(dbContextFactoryFake);

            await orderRepository.Create(new Order()
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
                    CustomerNumber = "CU00001",
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
                                Name = "testArticleGroup"
                            },
                            Description = "testArticleDescription",
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
                                Name = "testArticleGroup2"
                            },
                            Description = "testArticleDescription2",
                            Price = 21,
                        }
                    },
                },
            });
        }

        public static List<CustomerDto> GenerateCustomerDtoServiceTestData()
        {
            var list = new List<CustomerDto>()
            {
                new CustomerDto()
                {
                    Address = new AddressDto()
                    {
                        AddressId = 1,
                        BuildingNr = "2",
                        Street = "Teststrasse",
                        Town = new TownDto()
                        {
                            TownId = 1,
                            Townname = "Herisau",
                            ZipCode = "9100"
                        }
                    },
                    CustomerNumber = "CU00001",
                    AddressId = 1,
                    CustomerId = 1,
                    Email = "hans@test.ch",
                    Firstname = "Hans",
                    Lastname = "Müller",
                    Password = GetSecureString(),
                    Website = "www.hans.ch"
                },
                new CustomerDto()
                {
                    Address = new AddressDto()
                    {
                        AddressId = 2,
                        BuildingNr = "44",
                        Street = "Hauptstrasse",
                        Town = new TownDto()
                        {
                            TownId = 2,
                            Townname = "St. Gallen",
                            ZipCode = "9001"
                        }
                    },
                    CustomerNumber = "CU00002",
                    AddressId = 2,
                    CustomerId = 2,
                    Email = "ida@gmail.com",
                    Firstname = "Ida",
                    Lastname = "Muster",
                    Password = GetSecureString(),
                    Website = "www.ida.com"
                },
                new CustomerDto()
                {
                    Address = new AddressDto()
                    {
                        AddressId = 1,
                        BuildingNr = "2",
                        Street = "Teststrasse",
                        Town = new TownDto()
                        {
                            TownId = 1,
                            Townname = "Herisau",
                            ZipCode = "9100"
                        }
                    },
                    CustomerNumber = "CU00003",
                    AddressId = 1,
                    CustomerId = 3,
                    Email = "vreni@test.ch",
                    Firstname = "Vreni",
                    Lastname = "Müller",
                    Password = GetSecureString(),
                    Website = "www.vreni.ch"
                }
            };

            return list;
        }

        public static List<Customer> GenerateCustomerServiceTestData()
        {
            var list = new List<Customer>()
            {
                new Customer()
                {
                    Address = new Address()
                    {
                        AddressId = 1,
                        BuildingNr = "2",
                        Street = "Teststrasse",
                        Town = new Town()
                        {
                            TownId = 1,
                            Townname = "Herisau",
                            ZipCode = "9100"
                        }
                    },
                    CustomerNumber = "CU00001",
                    AddressId = 1,
                    CustomerId = 1,
                    Email = "hans@test.ch",
                    Firstname = "Hans",
                    Lastname = "Müller",
                    Password = new byte[70],
                    Website = "www.hans.ch"
                },
                new Customer()
                {
                    Address = new Address()
                    {
                        AddressId = 2,
                        BuildingNr = "44",
                        Street = "Hauptstrasse",
                        Town = new Town()
                        {
                            TownId = 2,
                            Townname = "St. Gallen",
                            ZipCode = "9001"
                        }
                    },
                    CustomerNumber = "CU00002",
                    AddressId = 2,
                    CustomerId = 2,
                    Email = "ida@gmail.com",
                    Firstname = "Ida",
                    Lastname = "Muster",
                    Password = new byte[70],
                    Website = "www.ida.com"
                },
                new Customer()
                {
                    Address = new Address()
                    {
                        AddressId = 1,
                        BuildingNr = "2",
                        Street = "Teststrasse",
                        Town = new Town()
                        {
                            TownId = 1,
                            Townname = "Herisau",
                            ZipCode = "9100"
                        }
                    },
                    CustomerNumber = "CU00003",
                    AddressId = 1,
                    CustomerId = 3,
                    Email = "vreni@test.ch",
                    Firstname = "Vreni",
                    Lastname = "Müller",
                    Password = new byte[70],
                    Website = "www.vreni.ch"
                }
            };

            return list;
        }

        public static List<Customer> GetCustomerTestData()
        {
            return new List<Customer>()
            {
                new Customer()
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
                    CustomerNumber = "CU00001",
                    Firstname = "Hans",
                    Lastname = "Müller",
                    Email = "hans@test.com",
                    Website = "www.hans.ch",
                    Password = new byte[64]
                },

                new Customer
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
                    CustomerNumber = "CU00002",
                    Firstname = "Ida",
                    Lastname = "Muster",
                    Email = "ida@gmail.com",
                    Website = "www.ida.com",
                    Password = new byte[64]
                },

                new Customer()
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
                    CustomerNumber = "CU00003",
                    Firstname = "Vreni",
                    Lastname = "Müller",
                    Email = "vreni@test.com",
                    Website = "www.vreni.ch",
                    Password = new byte[64]
                }
            };
        }

        public static List<Article> GetArticleTestData()
        {
            return new List<Article>()
            {
                new Article()
                {
                    ArticleGroup = new ArticleGroup()
                    {
                        Name = "testArticleGroup"
                    },
                    Description = "testArticleDescription",
                    Price = 22,
                },
                new Article()
                {
                    ArticleGroup = new ArticleGroup()
                    {
                        Name = "testArticleGroup2"
                    },
                    Description = "testArticleDescription2",
                    Price = 21,
                }
            };
        }

        public static IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            return mapper;
        }

        public static CustomerValidator GetCustomerValidator()
        {
            return new CustomerValidator();
        }

        public static SecureString GetSecureString()
        {
            char[] chars = { 't', 'E', 's', 't', '1', '2', '3', '4', '$' };
            SecureString secureString = new SecureString();
            foreach (char ch in chars)
                secureString.AppendChar(ch);

            return secureString;
        }
    }
}
