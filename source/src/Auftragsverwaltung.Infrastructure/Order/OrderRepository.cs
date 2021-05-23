using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Infrastructure.Order
{
    class OrderRepository : IAppRepository<Domain.Order>
    {

        private readonly AppDbContext _db;

        public OrderRepository(AppDbContextFactory dbContextFactory)
        {
            _db = dbContextFactory.CreateDbContext();
        }

        public async Task<Domain.Order> Create(Domain.Order entity)
        {
            EntityEntry<Domain.Order> createdEntity = await _db.Orders.AddAsync(entity);
            await _db.SaveChangesAsync();

            return createdEntity.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            Domain.Order entity = await _db.Orders.FirstOrDefaultAsync(e => e.OrderId == id);
            _db.Orders.Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Domain.Order> Get(int id)
        {
            Domain.Order entity = await _db.Orders.FirstOrDefaultAsync(e => e.OrderId == id);
            return entity;
        }

        public async Task<IEnumerable<Domain.Order>> GetAll()
        {
            List<Domain.Order> entities = await _db.Orders.ToListAsync();
            return entities;
        }

        public async Task<Domain.Order> Update(int id, Domain.Order entity)
        {
            entity.OrderId = id;
            _db.Orders.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
