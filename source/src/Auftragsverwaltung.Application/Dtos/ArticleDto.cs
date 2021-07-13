using Auftragsverwaltung.Domain.Article;
using Auftragsverwaltung.Domain.ArticleGroup;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Position;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.Application.Dtos
{
    public class ArticleDto : AppDto
    {
        public int ArticleId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ArticleGroupId { get; set; }
        public ArticleGroup ArticleGroup { get; set; }
        public Position Position { get; set; }
        public ResponseDto<Article> Response { get; set; }

        public ArticleDto()
        {
            ArticleGroup = new ArticleGroup();
        }

        public ArticleDto(Article article)
        {
            ArticleId = article.ArticleId;
            Description = article.Description;
            Price = article.Price;
            ArticleGroupId = article.ArticleGroupId;
            ArticleGroup = article.ArticleGroup;
            Position = article.Position;
        }

        public ArticleDto(ResponseDto<Article> response)
        {
            Response = response;
            if (response.Entity != null)
            {
                Response = response;
                ArticleId = response.Entity.ArticleId;
                Description = response.Entity.Description;
                Price = response.Entity.Price;
                ArticleGroupId = response.Entity.ArticleGroupId;
                ArticleGroup = response.Entity.ArticleGroup;
                Position = response.Entity.Position;
            }
            response.Entity = null;
        }
    }
}
