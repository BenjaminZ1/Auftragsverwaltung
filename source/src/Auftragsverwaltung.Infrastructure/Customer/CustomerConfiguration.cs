using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auftragsverwaltung.Infrastructure.Customer
{
    public class CustomerConfiguration : AppEntityConfigurations<Domain.Customer.Customer>
    {
        public override void Configure(EntityTypeBuilder<Domain.Customer.Customer> builder)
        {
            base.Configure(builder);

            builder.ToTable("Customer");
            builder
                .Property(c => c.Firstname).HasColumnType("varchar(255)");
            builder
                .Property(c => c.Lastname).HasColumnType("varchar(255)");
            builder
                .Property(c => c.Email).HasColumnType("varchar(255)");
            builder
                .Property(c => c.Website).HasColumnType("varchar(255)");
            builder
                .Property(c => c.Password).HasColumnType("binary(70)");
            builder
                .Property(c => c.CustomerNumber).HasColumnType("varchar(255)");

            builder.HasKey(c => c.CustomerId);

            builder
                .HasMany(c => c.Addresses)
                .WithOne(a => a.Customer);

            builder.HasData(
                new Domain.Customer.Customer
                {
                    CustomerId = 1,
                    CustomerNumber = "CU00001",
                    Firstname = "Hans",
                    Lastname = "Müller",
                    Email = "hans@test.com",
                    Website = "www.hans.ch",
                    Password = new byte[64]

                },
                new Domain.Customer.Customer
                {
                    CustomerId = 2,
                    CustomerNumber = "CU00002",
                    Firstname = "Ida",
                    Lastname = "Muster",
                    Email = "ida@gmail.com",
                    Website = "www.ida.com",
                    Password = new byte[64]
                },
                new Domain.Customer.Customer
                {
                    CustomerId = 3,
                    CustomerNumber = "CU00003",
                    Firstname = "Vreni",
                    Lastname = "Müller",
                    Email = "vreni@test.com",
                    Website = "www.vreni.ch",
                    Password = new byte[64]
                });

        }
    }
}
