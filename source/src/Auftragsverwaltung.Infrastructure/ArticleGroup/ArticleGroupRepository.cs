using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Auftragsverwaltung.Infrastructure.ArticleGroup
{
    public class ArticleGroupRepository : IAppRepository<Domain.ArticleGroup.ArticleGroup>
    {

        private readonly IServiceScopeFactory _scopeFactory;

        public ArticleGroupRepository(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<ResponseDto<Domain.ArticleGroup.ArticleGroup>> Create(Domain.ArticleGroup.ArticleGroup entity)
        {
            ResponseDto<Domain.ArticleGroup.ArticleGroup> response = new ResponseDto<Domain.ArticleGroup.ArticleGroup>();
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                EntityEntry<Domain.ArticleGroup.ArticleGroup> createdEntity = await db.ArticleGroups.AddAsync(entity);
                response.NumberOfRows = await db.SaveChangesAsync();

                response.Entity = createdEntity.Entity;
                response.Flag = true;
                response.Message = "Has been added.";
                response.Id = createdEntity.Entity.ArticleGroupId;
            }
            catch (Exception e)
            {
                response.Flag = false;
                response.Message = e.ToString();
            }

            return response;
        }

        public async Task<ResponseDto<Domain.ArticleGroup.ArticleGroup>> Delete(int id)
        {
            ResponseDto<Domain.ArticleGroup.ArticleGroup> response = new ResponseDto<Domain.ArticleGroup.ArticleGroup>();
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                Domain.ArticleGroup.ArticleGroup entity = await this.Get(id);
                db.ArticleGroups.Remove(entity);
                response.NumberOfRows = await db.SaveChangesAsync();

                response.Entity = entity;
                response.Flag = true;
                response.Message = "Has been deleted.";
                response.Id = entity.ArticleGroupId;
            }
            catch (Exception e)
            {
                response.Flag = false;
                response.Message = e.ToString();
            }

            return response;
        }

        public async Task<IEnumerable<Domain.ArticleGroup.ArticleGroup>> Search(string searchString)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            List<Domain.ArticleGroup.ArticleGroup> entities = await db.ArticleGroups
                .Where(e => e.Name.Contains(searchString))
                .ToListAsync();

            return entities;
        }

        public async Task<Domain.ArticleGroup.ArticleGroup> Get(int id)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            Domain.ArticleGroup.ArticleGroup entity = await db.ArticleGroups.FirstOrDefaultAsync(e => e.ArticleGroupId == id);
            return entity;
        }

        public async Task<IEnumerable<Domain.ArticleGroup.ArticleGroup>> GetAll()
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            List<Domain.ArticleGroup.ArticleGroup> entities = await db.ArticleGroups.ToListAsync();
            return entities;
        }

        public async Task<ResponseDto<Domain.ArticleGroup.ArticleGroup>> Update(Domain.ArticleGroup.ArticleGroup entity)
        {
            ResponseDto<Domain.ArticleGroup.ArticleGroup> response = new ResponseDto<Domain.ArticleGroup.ArticleGroup>();
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                db.ArticleGroups.Update(entity);
                response.NumberOfRows = await db.SaveChangesAsync();

                response.Entity = entity;
                response.Flag = true;
                response.Message = "Has been updated.";
                response.Id = entity.ArticleGroupId;

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
            var db = scope.ServiceProvider.GetService<AppDbContext>();

            Domain.ArticleGroup.ArticleGroup foundArticleGroup = await db.ArticleGroups
                .FirstOrDefaultAsync(e =>
                    e.Name == articleGroup.Name);

            return foundArticleGroup ?? articleGroup;
        }
    }
}
