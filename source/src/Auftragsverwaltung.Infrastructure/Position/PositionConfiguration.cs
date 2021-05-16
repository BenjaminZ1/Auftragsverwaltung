﻿using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auftragsverwaltung.Infrastructure.Position
{
    public class PositionConfiguration : AppEntityConfigurations<Domain.Position>
    {
        public override void Configure(EntityTypeBuilder<Domain.Position> builder)
        {
            base.Configure(builder);

            builder.ToTable("Position");
            builder.HasKey(p => new {p.OrderId, p.ArticleId });

            builder
                .HasOne(p => p.Order)
                .WithMany(o => o.Positions);
        }
    }
}
