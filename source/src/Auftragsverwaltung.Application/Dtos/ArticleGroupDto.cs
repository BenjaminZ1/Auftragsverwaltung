using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.Application.Dtos
{
    public class ArticleGroupDto : AppDto
    {
        public int ArticleGroupId { get; set; }
        public string Name { get; set; } = default!;
        public int? ParentArticleGroupId { get; set; }
        public virtual ICollection<ArticleDto> Articles { get; set; }
        public virtual ArticleGroupDto ParentArticleGroup { get; set; }
        public virtual ICollection<ArticleGroupDto> ChildArticlesGroups { get; set; }
    }
}
