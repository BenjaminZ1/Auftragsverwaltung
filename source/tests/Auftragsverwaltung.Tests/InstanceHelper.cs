using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Auftragsverwaltung.Repository.Tests
{
    public static class InstanceHelper
    {
        
        public static DbContextOptions<AppDbContext> AppDbContext_BuildDbContext()
        {
            return new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("testDb")
                .EnableSensitiveDataLogging()
                .Options;

            //_options = new DbContextOptionsBuilder<AppDbContext>()
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
    }
}
