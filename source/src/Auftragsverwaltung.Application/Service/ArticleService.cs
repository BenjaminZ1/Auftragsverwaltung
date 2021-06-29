using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Article;
using Auftragsverwaltung.Domain.Common;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Application.Service
{
    public class ArticleService : IArticleService
    {
        private readonly IAppRepository<Article> _repository;
        public ArticleService(IAppRepository<Article> repository)
        {
            _repository = repository;
        }

        public async Task<ArticleDto> Create(Article entity)
        {
            var response = await _repository.Create(entity);
            var mappedResponse = new ArticleDto(response);
            return mappedResponse;
        }

        public async Task<ArticleDto> Delete(int id)
        {
            var response = await _repository.Delete(id);
            var mappedResponse = new ArticleDto(response);
            return mappedResponse; 
        }

        public async Task<ArticleDto> Get(int id)
        {
            var data = await _repository.Get(id);
            var mappedData = new ArticleDto(data);
            return mappedData;
        }

        public async Task<IEnumerable<ArticleDto>> GetAll()
        {
            var data = await _repository.GetAll();
            var mappedData = data.Select(x => new ArticleDto(x));
            return mappedData;
        }

        public async Task<ArticleDto> Update(int id, Article entity)
        {
            var response = await _repository.Update(id, entity);
            var mappedResponse = new ArticleDto(response);
            return mappedResponse;
        }

        public Article ConvertToEntity(ArticleDto articleDto)
        {
            return new Article()
            {
                ArticleGroup = articleDto.ArticleGroup,
                Description = articleDto.Description,
                Position = articleDto.Position,
                Price = articleDto.Price
            };
        }
    }
}
