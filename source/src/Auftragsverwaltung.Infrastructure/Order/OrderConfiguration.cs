using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auftragsverwaltung.Infrastructure.Order
{
    public class OrderConfiguration : AppEntityConfigurations<Domain.Order>
    {
        public override void Configure(EntityTypeBuilder<Domain.Order> builder)
        {
            base.Configure(builder);

            builder.HasKey(o => o.OrderId);

            builder
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);
        }
    }
}
