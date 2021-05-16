using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auftragsverwaltung.Infrastructure.ArticleGroup
{
    public class ArticleGroupConfiguration : AppEntityConfigurations<Domain.ArticleGroup>
    {
        public override void Configure(EntityTypeBuilder<Domain.ArticleGroup> builder)
        {
            base.Configure(builder);

            builder
                .Property(ag => ag.Name).HasColumnType("varchar(255)");

            builder.HasKey(ag => ag.ArticleGroupId);

            builder
                .HasMany(ag => ag.Articles)
                .WithOne(a => a.ArticleGroup);

            builder
                .HasMany(ag => ag.ChildArticlesGroups)
                .WithOne(ag => ag.ParentArticleGroup)
                .HasForeignKey(ag => ag.ParentArticleGroupId)
                .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
        }
    }
}
