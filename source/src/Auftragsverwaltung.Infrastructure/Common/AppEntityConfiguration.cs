using Auftragsverwaltung.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auftragsverwaltung.Infrastructure.Common
{
    public class BaseEntityConfigurations<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
    {

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            //add configuration options for all entities
        }

    }
}
