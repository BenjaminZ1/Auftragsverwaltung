using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auftragsverwaltung.Infrastructure.Address
{
    public class AddressConfiguration : AppEntityConfigurations<Domain.Address>
    {
        public override void Configure(EntityTypeBuilder<Domain.Address> builder)
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
                .WithMany(t => t.Adresses)
                .HasForeignKey(a => a.TownId);
        }
    }
}
