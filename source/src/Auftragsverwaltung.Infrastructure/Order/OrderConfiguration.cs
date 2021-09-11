using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auftragsverwaltung.Infrastructure.Order
{
    public class OrderConfiguration : AppEntityConfigurations<Domain.Order.Order>
    {
        public override void Configure(EntityTypeBuilder<Domain.Order.Order> builder)
        {
            base.Configure(builder);

            builder.ToTable("Order");
            builder.HasKey(o => o.OrderId);

            builder
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            builder
                .HasMany(o => o.Positions)
                .WithOne(o => o.Order);

            builder.HasData(
                new Domain.Order.Order
                {
                    OrderId = 1,
                    Date = new System.DateTime(2021, 8, 25),
                    CustomerId = 1
                },
                new Domain.Order.Order
                {
                    OrderId = 2,
                    Date = new System.DateTime(2021, 8, 20),
                    CustomerId = 2
                },
                new Domain.Order.Order
                {
                    OrderId = 3,
                    Date = new System.DateTime(2020, 4, 20),
                    CustomerId = 2
                });
        }
    }
}
