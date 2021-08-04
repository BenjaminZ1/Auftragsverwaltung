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
        public ICollection<ArticleDto> Articles { get; set; }
        public ArticleGroupDto ParentArticleGroup { get; set; }
        public ICollection<ArticleGroupDto> ChildArticlesGroups { get; set; }
    }
}
