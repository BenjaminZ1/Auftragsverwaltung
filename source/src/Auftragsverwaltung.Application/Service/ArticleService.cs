using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Article;
using Auftragsverwaltung.Domain.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Auftragsverwaltung.Application.Service
{
    public class ArticleService : IArticleService
    {
        private readonly IAppRepository<Article> _repository;
        private readonly IMapper _mapper;
        public ArticleService(IAppRepository<Article> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ArticleDto> Create(ArticleDto dto)
        {
            var entity = _mapper.Map<Article>(dto);

            var response = await _repository.Create(entity);
            var mappedResponse = _mapper.Map<ArticleDto>(response);
            return mappedResponse;
        }

        public async Task<ArticleDto> Delete(int id)
        {
            var response = await _repository.Delete(id);
            var mappedResponse = _mapper.Map<ArticleDto>(response);
            return mappedResponse; 
        }

        public async Task<ArticleDto> Get(int id)
        {
            var data = await _repository.Get(id);
            var mappedData = _mapper.Map<ArticleDto>(data);
            return mappedData;
        }

        public async Task<IEnumerable<ArticleDto>> GetAll()
        {
            var data = await _repository.GetAll();
            var mappedData = data.Select(x => _mapper.Map<ArticleDto>(x));
            return mappedData;
        }

        public async Task<ArticleDto> Update(ArticleDto dto)
        {
            var entity = _mapper.Map<Article>(dto);

            var response = await _repository.Update(entity);
            var mappedResponse = _mapper.Map<ArticleDto>(response);
            return mappedResponse;
        }
    }
}
