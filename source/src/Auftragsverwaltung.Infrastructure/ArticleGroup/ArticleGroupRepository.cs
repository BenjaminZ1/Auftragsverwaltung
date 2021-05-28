using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public async Task<ResponseDto<Domain.ArticleGroup>> Create(Domain.ArticleGroup entity)
        {
            ResponseDto<Domain.ArticleGroup> response = new ResponseDto<Domain.ArticleGroup>();
            try
            {
                EntityEntry<Domain.ArticleGroup> createdEntity = await _db.ArticleGroups.AddAsync(entity);
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

        public async Task<ResponseDto<Domain.ArticleGroup>> Delete(int id)
        {
            ResponseDto<Domain.ArticleGroup> response = new ResponseDto<Domain.ArticleGroup>();
            try
            {
                Domain.ArticleGroup entity = await _db.ArticleGroups.FirstOrDefaultAsync(e => e.ArticleGroupId == id);
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

        public async Task<Domain.ArticleGroup> Get(int id)
        {
            Domain.ArticleGroup entity = await _db.ArticleGroups.FirstOrDefaultAsync(e => e.ArticleGroupId == id);
            return entity;
        }

        public async Task<IEnumerable<Domain.ArticleGroup>> GetAll()
        {
            List<Domain.ArticleGroup> entities = await _db.ArticleGroups.ToListAsync();
            return entities;
        }

        public async Task<ResponseDto<Domain.ArticleGroup>> Update(int id, Domain.ArticleGroup entity)
        {
            ResponseDto<Domain.ArticleGroup> response = new ResponseDto<Domain.ArticleGroup>();
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
