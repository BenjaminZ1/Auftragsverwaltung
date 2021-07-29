﻿using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Article;
using Auftragsverwaltung.Domain.Common;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ArticleDto> Create(ArticleDto dto)
        {
            var entity = ConvertToEntity(dto);

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

        public async Task<ArticleDto> Update(ArticleDto dto)
        {
            var entity = ConvertToEntity(dto);

            var response = await _repository.Update(entity);
            var mappedResponse = new ArticleDto(response);
            return mappedResponse;
        }

        internal Article ConvertToEntity(ArticleDto articleDto)
        {
            return new Article()
            {
                ArticleId = articleDto.ArticleId,
                //ArticleGroup = articleDto.ArticleGroup,
                ArticleGroupId = articleDto.ArticleGroupId,
                Description = articleDto.Description,
                //Position = articleDto.Position,
                Price = articleDto.Price
            };
        }
    }
}
