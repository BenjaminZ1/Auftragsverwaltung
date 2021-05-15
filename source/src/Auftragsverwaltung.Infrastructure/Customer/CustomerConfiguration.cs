using Auftragsverwaltung.Domain;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auftragsverwaltung.Infrastructure.Customer
{
    public class CustomerConfiguration : BaseEntityConfigurations<Domain.Customer>
    {
        public override void Configure(EntityTypeBuilder<Domain.Customer> builder)
        {
            base.Configure(builder);

            builder
                .Property(c => c.Firstname).HasColumnType("varchar(255)");
            builder
                .Property(c => c.Lastname).HasColumnType("varchar(255)");
            builder
                .Property(c => c.Email).HasColumnType("varchar(255)");
            builder
                .Property(c => c.Website).HasColumnType("varchar(255)");
            builder
                .Property(c => c.Password).HasColumnType("binary(64)");

            builder.HasKey(c => c.CustomerId);


            builder
                .HasOne(c => c.Adress)
                .WithMany(a => a.Customers)
                .HasForeignKey(c => c.Adress);
        }
    }
}
