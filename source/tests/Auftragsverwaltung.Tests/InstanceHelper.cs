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
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;

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

        public static List<CustomerDto> GenerateCustomerDtoServiceTestData()
        {
            var list = new List<CustomerDto>()
            {
                new CustomerDto()
                {
                    Addresses = new List<AddressDto>()
                    {
                        new AddressDto()
                        {
                            Street = "Teststrasse",
                            BuildingNr = "2",
                            ValidFrom = DateTime.Now,
                            ValidUntil = DateTime.MaxValue,
                            CustomerId = 1,
                            Town = new TownDto()
                            {
                                Townname = "Herisau",
                                ZipCode = "9100"
                            }
                        }
                    },
                    CustomerNumber = "CU00001",
                    CustomerId = 1,
                    Email = "hans@test.ch",
                    Firstname = "Hans",
                    Lastname = "Müller",
                    Password = GetSecureString(),
                    Website = "www.hans.ch"
                },
                new CustomerDto()
                {
                    Addresses = new List<AddressDto>()
                    {
                        new AddressDto()
                        {
                            AddressId = 2,
                            BuildingNr = "44",
                            Street = "Hauptstrasse",
                            ValidFrom = DateTime.Now,
                            ValidUntil = DateTime.MaxValue,
                            CustomerId = 2,
                            Town = new TownDto()
                            {
                                TownId = 2,
                                Townname = "St. Gallen",
                                ZipCode = "9001"
                            }
                        }
                    },
                    CustomerNumber = "CU00002",
                    CustomerId = 2,
                    Email = "ida@gmail.com",
                    Firstname = "Ida",
                    Lastname = "Muster",
                    Password = GetSecureString(),
                    Website = "www.ida.com"
                },
                new CustomerDto()
                {
                    Addresses = new List<AddressDto>()
                    {
                        new AddressDto() 
                        {
                            AddressId = 1,
                            BuildingNr = "2",
                            Street = "Teststrasse",
                            ValidFrom = DateTime.Now,
                            ValidUntil = DateTime.MaxValue,
                            CustomerId = 3,
                            Town = new TownDto()
                            {
                                TownId = 1,
                                Townname = "Herisau",
                                ZipCode = "9100"
                            }
                        }
                    },
                    CustomerNumber = "CU00003",
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
                    Addresses = new List<Address>()
                    {
                        new Address()
                        {
                            AddressId = 1,
                            BuildingNr = "2",
                            Street = "Teststrasse",
                            ValidFrom = DateTime.Now,
                            ValidUntil = DateTime.MaxValue,
                            CustomerId = 1,
                            Town = new Town()
                            {
                                TownId = 1,
                                Townname = "Herisau",
                                ZipCode = "9100"
                            }

                        }
                    },
                    CustomerNumber = "CU00001",
                    CustomerId = 1,
                    Email = "hans@test.ch",
                    Firstname = "Hans",
                    Lastname = "Müller",
                    Password = new byte[70],
                    Website = "www.hans.ch"
                },
                new Customer()
                {
                    Addresses = new List<Address>()
                    {
                        new Address()
                        {
                            AddressId = 2,
                            BuildingNr = "44",
                            Street = "Hauptstrasse",
                            ValidFrom = DateTime.Now,
                            ValidUntil = DateTime.MaxValue,
                            CustomerId = 2,
                            Town = new Town()
                            {
                                TownId = 2,
                                Townname = "St. Gallen",
                                ZipCode = "9001"
                            }
                        }
                    },
                    CustomerNumber = "CU00002",
                    CustomerId = 2,
                    Email = "ida@gmail.com",
                    Firstname = "Ida",
                    Lastname = "Muster",
                    Password = new byte[70],
                    Website = "www.ida.com"
                },
                new Customer()
                {
                    Addresses = new List<Address>()
                    {
                        new Address()
                        {
                            AddressId = 1,
                            BuildingNr = "2",
                            Street = "Teststrasse",
                            ValidFrom = DateTime.Now,
                            ValidUntil = DateTime.MaxValue,
                            CustomerId = 3,
                            Town = new Town()
                            {
                                TownId = 1,
                                Townname = "Herisau",
                                ZipCode = "9100"
                            }
                        }
                    },
                    CustomerNumber = "CU00003",
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
                    Addresses = new List<Address>()
                    {
                        new Address()
                        {
                            Street = "Teststrasse",
                            BuildingNr = "2",
                            ValidFrom = DateTime.Now,
                            ValidUntil = DateTime.MaxValue,
                            Town = new Town()
                            {
                                Townname = "Herisau",
                                ZipCode = "9100"
                            }
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
                    Addresses = new List<Address>()
                    {
                        new Address()
                        {
                            Street = "Hauptstrasse",
                            BuildingNr = "44",
                            ValidFrom = DateTime.Now,
                            ValidUntil = DateTime.MaxValue,
                            Town = new Town()
                            {
                                Townname = "St. Gallen",
                                ZipCode = "9001"
                            }
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
                    Addresses = new List<Address>()
                    {
                        new Address()
                        {
                            Street = "Teststrasse",
                            BuildingNr = "2",
                            ValidFrom = DateTime.Now,
                            ValidUntil = DateTime.MaxValue,
                            Town = new Town()
                            {
                                Townname = "Herisau",
                                ZipCode = "9100"
                            }
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
