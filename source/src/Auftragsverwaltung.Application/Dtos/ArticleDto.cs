using Auftragsverwaltung.Domain.Article;
using Auftragsverwaltung.Domain.Common;

namespace Auftragsverwaltung.Application.Dtos
{
    public class ArticleDto : AppDto
    {
        public int ArticleId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ArticleGroupId { get; set; }
        public ArticleGroupDto ArticleGroup { get; set; }
        public PositionDto Position { get; set; }
        public ResponseDto<Article> Response { get; set; }

        public ArticleDto()
        {
            ArticleGroup = new ArticleGroupDto();
        }
    }
}
