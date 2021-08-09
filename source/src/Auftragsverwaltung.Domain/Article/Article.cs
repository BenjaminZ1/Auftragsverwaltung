using System.Collections.Generic;
using Auftragsverwaltung.Domain.Common;

namespace Auftragsverwaltung.Domain.Article
{
    public class Article : EntityBase
    {
        public int ArticleId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ArticleGroupId { get; set; }
        public virtual ArticleGroup.ArticleGroup ArticleGroup { get; set; } = default!;
        public virtual ICollection<Position.Position> Positions { get; set; } 
    }
}
