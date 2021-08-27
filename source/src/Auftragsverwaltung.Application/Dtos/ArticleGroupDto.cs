using Auftragsverwaltung.Domain.ArticleGroup;
using Auftragsverwaltung.Domain.Common;
using System.Collections.Generic;

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
        public ResponseDto<ArticleGroup> Response { get; set; }
    }
}
