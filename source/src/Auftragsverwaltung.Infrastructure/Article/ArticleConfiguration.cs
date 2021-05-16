using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auftragsverwaltung.Infrastructure.Article
{
    public class ArticleConfiguration : AppEntityConfigurations<Domain.Article>
    {
        public override void Configure(EntityTypeBuilder<Domain.Article> builder)
        {
            base.Configure(builder);

            builder.ToTable("Article");
            builder
                .Property(a => a.Description).HasColumnType("varchar(255)");
            builder
                .Property(a => a.Price).HasColumnType("decimal(10,2)");

            builder.HasKey(a => a.ArticleId);

            builder
                .HasOne(a => a.Position)
                .WithOne(p => p.Article);
        }
    }
}
