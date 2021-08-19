using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Article;
using Auftragsverwaltung.Domain.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auftragsverwaltung.Domain.ArticleGroup;
using AutoMapper;

namespace Auftragsverwaltung.Application.Service
{
    public class ArticleGroupService : IArticleGroupService
    {
        private readonly IAppRepository<ArticleGroup> _repository;
        private readonly IMapper _mapper;
        public ArticleGroupService(IAppRepository<ArticleGroup> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ArticleGroupDto> Create(ArticleGroupDto dto)
        {
            var entity = _mapper.Map<ArticleGroup>(dto);

            var response = await _repository.Create(entity);
            var mappedResponse = _mapper.Map<ArticleGroupDto>(response);
            return mappedResponse;
        }

        public async Task<ArticleGroupDto> Delete(int id)
        {
            var response = await _repository.Delete(id);
            var mappedResponse = _mapper.Map<ArticleGroupDto>(response);
            return mappedResponse; 
        }

        public async Task<ArticleGroupDto> Get(int id)
        {
            var data = await _repository.Get(id);
            var mappedData = _mapper.Map<ArticleGroupDto>(data);
            return mappedData;
        }

        public async Task<IEnumerable<ArticleGroupDto>> GetAll()
        {
            var data = await _repository.GetAll();
            var mappedData = data.Select(x => _mapper.Map<ArticleGroupDto>(x));
            return mappedData;
        }

        public async Task<ArticleGroupDto> Update(ArticleGroupDto dto)
        {
            var entity = _mapper.Map<ArticleGroup>(dto);

            var response = await _repository.Update(entity);
            var mappedResponse = _mapper.Map<ArticleGroupDto>(response);
            return mappedResponse;
        }
    }
}
