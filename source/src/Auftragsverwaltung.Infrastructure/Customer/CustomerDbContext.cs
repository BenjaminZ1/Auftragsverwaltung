using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Auftragsverwaltung.Infrastructure.Customer
{
    class CustomerDbContext : BaseDbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }
        public DbSet<Domain.Customer> Customers { get; set; }
    }
}
