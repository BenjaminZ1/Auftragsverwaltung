using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Auftragsverwaltung.Infrastructure.Common
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args = null)
        {
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", true)
            //    .AddEnvironmentVariables()
            //    .Build();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = "Data Source=.\\ZBW; Database=Auftragsverwaltung; Trusted_Connection=True";

            builder.UseSqlServer(connectionString,
                x => x.MigrationsAssembly(typeof(AppDbContextFactory).Assembly.FullName));

            return new AppDbContext(builder.Options);
        }
    }
}
