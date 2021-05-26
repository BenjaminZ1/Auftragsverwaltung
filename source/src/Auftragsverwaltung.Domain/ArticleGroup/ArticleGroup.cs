using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Domain.Common;

namespace Auftragsverwaltung.Domain
{
    public class ArticleGroup : EntityBase
    {
#nullable enable
        public int ArticleGroupId { get; set; }
        public string Name { get; set; }
        public int? ParentArticleGroupId { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ArticleGroup? ParentArticleGroup { get; set; }
        public virtual ICollection<ArticleGroup> ChildArticlesGroups { get; set; }
#nullable disable
    }
}
