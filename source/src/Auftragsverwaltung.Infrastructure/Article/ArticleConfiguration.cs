using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auftragsverwaltung.Infrastructure.Article
{
    public class ArticleConfiguration : AppEntityConfigurations<Domain.Article.Article>
    {
        public override void Configure(EntityTypeBuilder<Domain.Article.Article> builder)
        {
            base.Configure(builder);

            builder.ToTable("Article");
            builder
                .Property(a => a.Description).HasColumnType("varchar(255)");
            builder
                .Property(a => a.Price).HasColumnType("decimal(10,2)");

            builder.HasKey(a => a.ArticleId);

            builder
                .HasMany(a => a.Positions)
                .WithOne(p => p.Article);
        }
    }
}
