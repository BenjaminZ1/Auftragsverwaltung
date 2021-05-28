using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Infrastructure.ArticleGroup
{
    public class ArticleGroupRepository : IAppRepository<Domain.ArticleGroup>
    {

        private readonly AppDbContext _db;

        public ArticleGroupRepository(AppDbContextFactory dbContextFactory)
        {
            _db = dbContextFactory.CreateDbContext();
        }

        public Task<ResponseDto<Domain.ArticleGroup>> Create(Domain.ArticleGroup entity)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<Domain.ArticleGroup>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.ArticleGroup> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.ArticleGroup>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<Domain.ArticleGroup>> Update(int id, Domain.ArticleGroup entity)
        {
            throw new NotImplementedException();
        }
    }
}
