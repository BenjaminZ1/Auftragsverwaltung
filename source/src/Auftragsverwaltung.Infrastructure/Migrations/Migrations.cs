using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Auftragsverwaltung.Infrastructure.Migrations
{
    public class Migrations
    {
        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<BaseDbContext>
        {
            public BaseDbContext CreateDbContext(string[] args)
            {
                //var configuration = new ConfigurationBuilder()
                //    .SetBasePath(Directory.GetCurrentDirectory())
                //    .AddJsonFile("appsettings.json", true)
                //    .AddEnvironmentVariables()
                //    .Build();

                var builder = new DbContextOptionsBuilder<BaseDbContext>();

                var connectionString = "Data Source=.\\ZBW; Database=Auftragsverwaltung; Trusted_Connection=True";

                builder.UseSqlServer(connectionString,
                    x => x.MigrationsAssembly(typeof(ApplicationDbContextFactory).Assembly.FullName));


                return new BaseDbContext(builder.Options);
            }
        }
    }
}
