using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auftragsverwaltung.Infrastructure.Town
{
    public class TownConfiguration : AppEntityConfigurations<Domain.Town.Town>
    {
        public override void Configure(EntityTypeBuilder<Domain.Town.Town> builder)
        {
            base.Configure(builder);

            builder.ToTable("Town");
            builder
                .Property(t => t.ZipCode).HasColumnType("varchar(20)");
            builder
                .Property(t => t.Townname).HasColumnType("varchar(85)");

            builder.HasKey(t => t.TownId);

            builder
                .HasMany(t => t.Addresses)
                .WithOne(a => a.Town);

            builder.HasData(
                new Domain.Town.Town
                {
                    TownId = 1,
                    Townname = "Heerbrugg",
                    ZipCode = "9435"
                },
                new Domain.Town.Town
                {
                    TownId = 2,
                    Townname = "Widnau",
                    ZipCode = "9443"
                });
        }
    }
}
