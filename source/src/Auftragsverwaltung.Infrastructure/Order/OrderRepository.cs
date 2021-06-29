using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Infrastructure.Order
{
    public class OrderRepository : IAppRepository<Domain.Order.Order>
    {

        private readonly AppDbContext _db;

        public OrderRepository(AppDbContextFactory dbContextFactory)
        {
            _db = dbContextFactory.CreateDbContext();
        }

        public async Task<ResponseDto<Domain.Order.Order>> Create(Domain.Order.Order entity)
        {
            ResponseDto<Domain.Order.Order> response = new ResponseDto<Domain.Order.Order>();
            try
            {
                EntityEntry<Domain.Order.Order> createdEntity = await _db.Orders.AddAsync(entity);
                response.NumberOfRows = await _db.SaveChangesAsync();

                response.Entity = createdEntity.Entity;
                response.Flag = true;
                response.Message = "Has been added.";
                response.Id = createdEntity.Entity.OrderId;

            }
            catch (Exception e)
            {
                response.Flag = false;
                response.Message = e.ToString();
            }

            return response;
        }

        public async Task<ResponseDto<Domain.Order.Order>> Delete(int id)
        {
            ResponseDto<Domain.Order.Order> response = new ResponseDto<Domain.Order.Order>();
            try
            {
                Domain.Order.Order entity = await _db.Orders.FirstOrDefaultAsync(e => e.OrderId == id);
                _db.Orders.Remove(entity);
                response.NumberOfRows = await _db.SaveChangesAsync();

                response.Entity = entity;
                response.Flag = true;
                response.Message = "Has been deleted.";
                response.Id = entity.OrderId;

            }
            catch (Exception e)
            {
                response.Flag = false;
                response.Message = e.ToString();
            }

            return response;
        }

        public async Task<IEnumerable<Domain.Order.Order>> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Order.Order> Get(int id)
        {
            Domain.Order.Order entity = await _db.Orders
                .Include(o => o.Positions)
                .ThenInclude(o => o.Article)
                .ThenInclude(o => o.ArticleGroup)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(e => e.OrderId == id);
            return entity;
        }

        public async Task<IEnumerable<Domain.Order.Order>> GetAll()
        {
            List<Domain.Order.Order> entities = await _db.Orders
                .Include(o => o.Positions)
                .ThenInclude(o => o.Article)
                .ThenInclude(o => o.ArticleGroup)
                .Include(o => o.Customer)
                .ToListAsync();
            return entities;
        }

        public async Task<ResponseDto<Domain.Order.Order>> Update(Domain.Order.Order entity)
        {
            ResponseDto<Domain.Order.Order> response = new ResponseDto<Domain.Order.Order>();
            try
            {
                var entry = await this.Get(entity.CustomerId);
                _db.Entry(entry).CurrentValues.SetValues(entity);
                response.NumberOfRows = await _db.SaveChangesAsync();

                response.Entity = entity;
                response.Flag = true;
                response.Message = "Has been updated.";
                response.Id = entity.OrderId;

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
