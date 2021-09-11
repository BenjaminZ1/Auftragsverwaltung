using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Auftragsverwaltung.Infrastructure.Address
{
    public class AddressConfiguration : AppEntityConfigurations<Domain.Address.Address>
    {
        public override void Configure(EntityTypeBuilder<Domain.Address.Address> builder)
        {
            base.Configure(builder);

            builder.ToTable("Address");
            builder
                .Property(a => a.Street).HasColumnType("varchar(255)");
            builder
                .Property(a => a.BuildingNr).HasColumnType("varchar(50)");

            builder.HasKey(a => a.AddressId);

            builder
                .HasOne(a => a.Town)
                .WithMany(t => t.Addresses)
                .HasForeignKey(a => a.TownId);

            builder.HasData(
                new Domain.Address.Address
                {
                    AddressId = 1,
                    Street = "Jumbostrasse",
                    BuildingNr = "69",
                    ValidFrom = new System.DateTime(2020, 08, 30),
                    ValidUntil = DateTime.MaxValue,
                    TownId = 1,
                    CustomerId = 1
                },
                new Domain.Address.Address
                {
                    AddressId = 2,
                    Street = "Wumbostrasse",
                    BuildingNr = "420",
                    ValidFrom = new System.DateTime(2019, 04, 20),
                    ValidUntil = new System.DateTime(2021, 01, 01),
                    TownId = 2,
                    CustomerId = 2
                },
                new Domain.Address.Address
                {
                    AddressId = 3,
                    Street = "Jumbostrasse",
                    BuildingNr = "69",
                    ValidFrom = new System.DateTime(2020, 08, 30),
                    ValidUntil = DateTime.MaxValue,
                    TownId = 1,
                    CustomerId = 3
                },
                new Domain.Address.Address
                {
                    AddressId = 4,
                    Street = "Dumbostrasse",
                    BuildingNr = "42",
                    ValidFrom = new System.DateTime(2021, 01, 02, 23, 59, 59),
                    ValidUntil = DateTime.MaxValue,
                    TownId = 2,
                    CustomerId = 2
                });
        }
    }
}
