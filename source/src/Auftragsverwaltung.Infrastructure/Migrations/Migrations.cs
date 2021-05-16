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
        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
        {
            public AppDbContext CreateDbContext(string[] args)
            {
                //var configuration = new ConfigurationBuilder()
                //    .SetBasePath(Directory.GetCurrentDirectory())
                //    .AddJsonFile("appsettings.json", true)
                //    .AddEnvironmentVariables()
                //    .Build();

                var builder = new DbContextOptionsBuilder<AppDbContext>();

                var connectionString = "Data Source=.\\ZBW; Database=Auftragsverwaltung; Trusted_Connection=True";

                builder.UseSqlServer(connectionString,
                    x => x.MigrationsAssembly(typeof(ApplicationDbContextFactory).Assembly.FullName));


                return new AppDbContext(builder.Options);
            }
        }
    }
}
