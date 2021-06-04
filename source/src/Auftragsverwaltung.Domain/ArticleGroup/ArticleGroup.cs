using Auftragsverwaltung.Domain.Common;
using System.Collections.Generic;

namespace Auftragsverwaltung.Domain.ArticleGroup
{
    public class ArticleGroup : EntityBase
    {
#nullable enable
        public int ArticleGroupId { get; set; }
        public string Name { get; set; } = default!;
        public int? ParentArticleGroupId { get; set; }
        public virtual ICollection<Article.Article> Articles { get; set; } = default!;
        public virtual ArticleGroup? ParentArticleGroup { get; set; }
        public virtual ICollection<ArticleGroup> ChildArticlesGroups { get; set; } = default!;
#nullable disable
    }
}
