using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Infrastructure.Article
{
    public class ArticleRepository : IAppRepository<Domain.Article.Article>
    {

        private readonly IServiceScopeFactory _scopeFactory;

        public ArticleRepository(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<ResponseDto<Domain.Article.Article>> Create(Domain.Article.Article entity)
        {
            ResponseDto<Domain.Article.Article> response = new ResponseDto<Domain.Article.Article>();
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                //entity.ArticleGroup = await FindOrAddNewArticleGroup(entity.ArticleGroup);
                entity.ArticleGroup = await GetArticleGroup(entity.ArticleGroup, db);

                EntityEntry<Domain.Article.Article> createdEntity = await db.Articles.AddAsync(entity);
                response.NumberOfRows = await db.SaveChangesAsync();

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

        public async Task<ResponseDto<Domain.Article.Article>> Delete(int id)
        {
            ResponseDto<Domain.Article.Article> response = new ResponseDto<Domain.Article.Article>();
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                Domain.Article.Article entity = await db.Articles.FirstOrDefaultAsync(e => e.ArticleId == id);
                db.Articles.Remove(entity);
                response.NumberOfRows = await db.SaveChangesAsync();

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
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            List<Domain.Article.Article> entities = await db.Articles
                .Include(a => a.ArticleGroup)
                .Where(e => e.Description.Contains(searchString))
                .ToListAsync();

            return entities;
        }

        public async Task<Domain.Article.Article> Get(int id)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            Domain.Article.Article entity = await db.Articles
                .Include(a => a.ArticleGroup)
                .FirstOrDefaultAsync(e => e.ArticleId == id);

            return entity;
        }

        public async Task<IEnumerable<Domain.Article.Article>> GetAll()
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            List<Domain.Article.Article> entities = await db.Articles
                .Include(a => a.ArticleGroup)
                .ToListAsync();

            return entities;
        }

        public async Task<ResponseDto<Domain.Article.Article>> Update(Domain.Article.Article entity)
        {
            ResponseDto<Domain.Article.Article> response = new ResponseDto<Domain.Article.Article>();
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                entity.ArticleGroup = await FindOrAddNewArticleGroup(entity.ArticleGroup);
                db.Articles.Update(entity);
                response.NumberOfRows = await db.SaveChangesAsync();

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

        private async Task<Domain.ArticleGroup.ArticleGroup> FindOrAddNewArticleGroup(Domain.ArticleGroup.ArticleGroup articleGroup)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            Domain.ArticleGroup.ArticleGroup foundArticleGroup = await db.ArticleGroups.FirstOrDefaultAsync(e =>
                e.Name == articleGroup.Name);

            if (articleGroup.ParentArticleGroup != null)
                articleGroup.ParentArticleGroup = await FindOrAddNewParentArticleGroup(articleGroup.ParentArticleGroup);

            return foundArticleGroup ?? articleGroup;
        }

        private async Task<Domain.ArticleGroup.ArticleGroup> FindOrAddNewParentArticleGroup(Domain.ArticleGroup.ArticleGroup articleGroup)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            Domain.ArticleGroup.ArticleGroup foundArticleGroup = await db.ArticleGroups.FirstOrDefaultAsync(e =>
                e.Name == articleGroup.Name);

            return foundArticleGroup ?? articleGroup;
        }

        private async Task<Domain.ArticleGroup.ArticleGroup> GetArticleGroup(Domain.ArticleGroup.ArticleGroup articleGroup, AppDbContext db)
        {
            var existingArticleGroup = await db.ArticleGroups
                .FirstOrDefaultAsync(e => e.ArticleGroupId == articleGroup.ArticleGroupId);

            return existingArticleGroup;
        }
    }
}
