using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Auftragsverwaltung.Infrastructure.Common
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public virtual AppDbContext CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = "Data Source=.\\ZBW; Database=Auftragsverwaltung; Trusted_Connection=True";

            //var connectionString = "Data Source=devlvl.com; Database=Auftragsverwaltung; User Id=sa; Password=Zbw2021*";

            builder.UseSqlServer(connectionString,
                x => x.MigrationsAssembly(typeof(AppDbContextFactory).Assembly.FullName));

            return new AppDbContext(builder.Options);
        }
    }
}
