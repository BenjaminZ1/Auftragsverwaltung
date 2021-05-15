using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Infrastructure.Common;
using Auftragsverwaltung.Infrastructure.Customer;
using Microsoft.EntityFrameworkCore;

namespace Auftragsverwaltung.Infrastructure.Order
{
    public class OrderDbContext : BaseDbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }
        public DbSet<Domain.Order> Orders { get; set; }
        public DbSet<Domain.Position> Positions { get; set; }
    }
}
