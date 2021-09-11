using System.Collections.Generic;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auftragsverwaltung.Infrastructure.Position
{
    public class PositionConfiguration : AppEntityConfigurations<Domain.Position.Position>
    {
        public override void Configure(EntityTypeBuilder<Domain.Position.Position> builder)
        {
            base.Configure(builder);

            builder.ToTable("Position");
            builder.HasKey(p => new { p.OrderId, p.ArticleId });

            builder
                .HasOne(p => p.Order)
                .WithMany(o => o.Positions);

            builder.HasData(
                new List<Domain.Position.Position>
                {
                    new Domain.Position.Position
                    {
                        ArticleId = 1,
                        OrderId = 1,
                        Amount = 2
                    },
                    new Domain.Position.Position
                    {
                        ArticleId = 2,
                        OrderId = 1,
                        Amount = 4
                    },
                    new Domain.Position.Position
                    {
                        ArticleId = 1,
                        OrderId = 2,
                        Amount = 12
                    },
                    new Domain.Position.Position
                    {
                        ArticleId = 2,
                        OrderId = 3,
                        Amount = 24
                    },
                });
        }
    }
}
