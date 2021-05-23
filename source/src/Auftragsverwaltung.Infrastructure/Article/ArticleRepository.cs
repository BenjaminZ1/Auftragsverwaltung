using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Auftragsverwaltung.Infrastructure.Article
{
    class ArticleRepository : IAppRepository<Domain.Article>
    {

        private readonly AppDbContext _db;

        public Task<Domain.Article> Create(Domain.Article entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int id)
        {
            Domain.Article entity = await _db.Articles.FirstOrDefaultAsync(e => e.ArticleId == id);
            _db.Articles.Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Domain.Article> Get(int id)
        {
            Domain.Article entity = await _db.Articles.FirstOrDefaultAsync(e => e.ArticleId == id);
            return entity;
        }

        public async Task<IEnumerable<Domain.Article>> GetAll()
        {
            List<Domain.Article> entities = await _db.Articles.ToListAsync();
            return entities;
        }

        public async Task<Domain.Article> Update(int id, Domain.Article entity)
        {
            entity.ArticleId = id;
            _db.Articles.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
