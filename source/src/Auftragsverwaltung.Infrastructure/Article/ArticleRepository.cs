using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Infrastructure.Article
{
    public class ArticleRepository : IAppRepository<Domain.Article.Article>
    {

        private readonly AppDbContext _db;

        public ArticleRepository(AppDbContextFactory dbContextFactory)
        {
            _db = dbContextFactory.CreateDbContext();
        }

        public async Task<ResponseDto<Domain.Article.Article>> Create(Domain.Article.Article entity)
        {
            ResponseDto<Domain.Article.Article> response = new ResponseDto<Domain.Article.Article>();
            try
            {
                entity.ArticleGroup = await FindOrAddNewArticleGroup(entity.ArticleGroup);

                EntityEntry<Domain.Article.Article> createdEntity = await _db.Articles.AddAsync(entity);
                response.NumberOfRows = await _db.SaveChangesAsync();

                response.Entity = createdEntity.Entity;
                response.Flag = true;
                response.Message = "Has been added.";
                response.Id = createdEntity.Entity.ArticleId;

            }
            catch (Exception e)
            {
                response.Flag = false;
                response.Message = e.ToString();
            }

            return response;
        }

        private async Task<Domain.ArticleGroup.ArticleGroup> FindOrAddNewArticleGroup(Domain.ArticleGroup.ArticleGroup articleGroup)
        {
            Domain.ArticleGroup.ArticleGroup foundArticleGroup = await _db.ArticleGroups.FirstOrDefaultAsync(e =>
                e.Name == articleGroup.Name);

            if (articleGroup.ParentArticleGroup != null)
                articleGroup.ParentArticleGroup = await FindOrAddNewParentArticleGroup(articleGroup.ParentArticleGroup);

            return foundArticleGroup ?? articleGroup;
        }

        private async Task<Domain.ArticleGroup.ArticleGroup> FindOrAddNewParentArticleGroup(Domain.ArticleGroup.ArticleGroup articleGroup)
        {
            Domain.ArticleGroup.ArticleGroup foundArticleGroup = await _db.ArticleGroups.FirstOrDefaultAsync(e =>
                e.Name == articleGroup.Name);

            return foundArticleGroup ?? articleGroup;
        }

        public async Task<ResponseDto<Domain.Article.Article>> Delete(int id)
        {
            ResponseDto<Domain.Article.Article> response = new ResponseDto<Domain.Article.Article>();
            try
            {
                Domain.Article.Article entity = await _db.Articles.FirstOrDefaultAsync(e => e.ArticleId == id);
                _db.Articles.Remove(entity);
                response.NumberOfRows = await _db.SaveChangesAsync();

                response.Entity = entity;
                response.Flag = true;
                response.Message = "Has been deleted.";
                response.Id = entity.ArticleId;

            }
            catch (Exception e)
            {
                response.Flag = false;
                response.Message = e.ToString();
            }

            return response;
        }

        public async Task<IEnumerable<Domain.Article.Article>> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Article.Article> Get(int id)
        {
            Domain.Article.Article entity = await _db.Articles
                .Include(a => a.ArticleGroup)
                .FirstOrDefaultAsync(e => e.ArticleId == id);
            return entity;
        }

        public async Task<IEnumerable<Domain.Article.Article>> GetAll()
        {
            List<Domain.Article.Article> entities = await _db.Articles.ToListAsync();
            return entities;
        }

        public async Task<ResponseDto<Domain.Article.Article>> Update(Domain.Article.Article entity)
        {
            ResponseDto<Domain.Article.Article> response = new ResponseDto<Domain.Article.Article>();
            try
            {
                _db.Articles.Update(entity);
                response.NumberOfRows = await _db.SaveChangesAsync();

                response.Entity = entity;
                response.Flag = true;
                response.Message = "Has been updated.";
                response.Id = entity.ArticleId;

            }
            catch (Exception e)
            {
                response.Flag = false;
                response.Message = e.ToString();
            }

            return response;
        }
    }
}
