using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Auftragsverwaltung.Infrastructure.Article
{
    class ArticleRepository : IAppRepository<Domain.Article>
    {

        private readonly AppDbContext _db;

        public ArticleRepository(AppDbContextFactory dbContextFactory)
        {
            _db = dbContextFactory.CreateDbContext();
        }

        public async Task<ResponseDto<Domain.Article>> Create(Domain.Article entity)
        {
            ResponseDto<Domain.Article> response = new ResponseDto<Domain.Article>();
            try
            {
                entity.ArticleGroup = await FindOrAddNewArticleGroup(entity.ArticleGroup);
                EntityEntry<Domain.Article> createdEntity = await _db.Articles.AddAsync(entity);
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

        private async Task<Domain.ArticleGroup> FindOrAddNewArticleGroup(Domain.ArticleGroup articleGroup)
        {
            Domain.ArticleGroup foundArticleGroup = await _db.ArticleGroups.FirstOrDefaultAsync(e =>
                e.Name == articleGroup.Name);

            return foundArticleGroup ?? articleGroup;
        }

        public async Task<ResponseDto<Domain.Article>> Delete(int id)
        {
            ResponseDto<Domain.Article> response = new ResponseDto<Domain.Article>();
            try
            {
                Domain.Article entity = await _db.Articles.FirstOrDefaultAsync(e => e.ArticleId == id);
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

        public async Task<ResponseDto<Domain.Article>> Update(int id, Domain.Article entity)
        {
            ResponseDto<Domain.Article> response = new ResponseDto<Domain.Article>();
            try
            {
                entity.ArticleId = id;
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
