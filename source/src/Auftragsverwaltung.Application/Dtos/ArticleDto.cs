using Auftragsverwaltung.Domain.Article;
using Auftragsverwaltung.Domain.ArticleGroup;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Position;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.Application.Dtos
{
    class ArticleDto : AppDto
    {
        public int ArticleId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ArticleGroupId { get; set; }
        public virtual ArticleGroup ArticleGroup { get; set; }
        public virtual Position Position { get; set; }
        public ResponseDto<Article> Response { get; set; }

        public ArticleDto(Article article)
        {
            ArticleId = article.ArticleId;
            Description = article.Description;
            Price = article.Price;
            ArticleGroupId = article.ArticleGroupId;
            Position = article.Position;
        }

        public ArticleDto(ResponseDto<Article> response)
        {
            Response = response;
            ArticleId = response.Entity.ArticleId;
            Description = response.Entity.Description;
            Price = response.Entity.Price;
            ArticleGroupId = response.Entity.ArticleGroupId;
            Position = response.Entity.Position;
        }
    }
}
