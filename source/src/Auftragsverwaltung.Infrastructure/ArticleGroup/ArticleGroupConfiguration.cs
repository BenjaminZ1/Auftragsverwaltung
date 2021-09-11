using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auftragsverwaltung.Infrastructure.ArticleGroup
{
    public class ArticleGroupConfiguration : AppEntityConfigurations<Domain.ArticleGroup.ArticleGroup>
    {
        public override void Configure(EntityTypeBuilder<Domain.ArticleGroup.ArticleGroup> builder)
        {
            base.Configure(builder);

            builder.ToTable("ArticleGroup");
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

            builder.HasData(
                new Domain.ArticleGroup.ArticleGroup
                {
                    ArticleGroupId = 1,
                    Name = "Pflegeprodukte"

                },
                new Domain.ArticleGroup.ArticleGroup
                {
                    ArticleGroupId = 2,
                    Name = "Haushaltsprodukte"

                },
                new Domain.ArticleGroup.ArticleGroup
                {
                    ArticleGroupId = 3,
                    Name = "Körperpflege",
                    ParentArticleGroupId = 1
                });
        }
    }
}
