using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Infrastructure.ArticleGroup
{
    public class ArticleGroupRepository : IAppRepository<Domain.ArticleGroup.ArticleGroup>
    {

        private readonly AppDbContext _db;

        public ArticleGroupRepository(AppDbContextFactory dbContextFactory)
        {
            _db = dbContextFactory.CreateDbContext();
        }

        public async Task<ResponseDto<Domain.ArticleGroup.ArticleGroup>> Create(Domain.ArticleGroup.ArticleGroup entity)
        {
            ResponseDto<Domain.ArticleGroup.ArticleGroup> response = new ResponseDto<Domain.ArticleGroup.ArticleGroup>();
            try
            {
                EntityEntry<Domain.ArticleGroup.ArticleGroup> createdEntity = await _db.ArticleGroups.AddAsync(entity);
                response.NumberOfRows = await _db.SaveChangesAsync();

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
                Domain.ArticleGroup.ArticleGroup entity = await _db.ArticleGroups.FirstOrDefaultAsync(e => e.ArticleGroupId == id);
                _db.ArticleGroups.Remove(entity);
                response.NumberOfRows = await _db.SaveChangesAsync();

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
            throw new NotImplementedException();
        }

        public async Task<Domain.ArticleGroup.ArticleGroup> Get(int id)
        {
            Domain.ArticleGroup.ArticleGroup entity = await _db.ArticleGroups.FirstOrDefaultAsync(e => e.ArticleGroupId == id);
            return entity;
        }

        public async Task<IEnumerable<Domain.ArticleGroup.ArticleGroup>> GetAll()
        {
            List<Domain.ArticleGroup.ArticleGroup> entities = await _db.ArticleGroups.ToListAsync();
            return entities;
        }

        public async Task<ResponseDto<Domain.ArticleGroup.ArticleGroup>> Update(int id, Domain.ArticleGroup.ArticleGroup entity)
        {
            ResponseDto<Domain.ArticleGroup.ArticleGroup> response = new ResponseDto<Domain.ArticleGroup.ArticleGroup>();
            try
            {
                entity.ArticleGroupId = id;
                _db.ArticleGroups.Update(entity);
                response.NumberOfRows = await _db.SaveChangesAsync();

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
    }
}
