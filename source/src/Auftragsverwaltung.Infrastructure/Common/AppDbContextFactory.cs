using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace Auftragsverwaltung.Infrastructure.Common
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public virtual AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("migrationsettings.json", false)
                .Build();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("default");
            builder.UseSqlServer(connectionString,
                x => x.MigrationsAssembly(typeof(AppDbContextFactory).Assembly.FullName));

            return new AppDbContext(builder.Options);
        }
    }
}
