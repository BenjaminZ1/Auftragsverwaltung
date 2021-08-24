using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auftragsverwaltung.Infrastructure.Position;
using Microsoft.Extensions.DependencyInjection;

namespace Auftragsverwaltung.Infrastructure.Order
{
    public class OrderRepository : IAppRepository<Domain.Order.Order>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public OrderRepository(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<ResponseDto<Domain.Order.Order>> Create(Domain.Order.Order entity)
        {
            ResponseDto<Domain.Order.Order> response = new ResponseDto<Domain.Order.Order>();
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                entity.Customer = await GetCustomer(entity.Customer, db);
                entity.Positions = await GetPositions(entity, db);

                EntityEntry<Domain.Order.Order> createdEntity = await db.Orders.AddAsync(entity);
                response.NumberOfRows = await db.SaveChangesAsync();

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
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                Domain.Order.Order entity = await db.Orders.FirstOrDefaultAsync(e => e.OrderId == id);
                db.Orders.Remove(entity);
                response.NumberOfRows = await db.SaveChangesAsync();

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
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            List<Domain.Order.Order> entities = await db.Orders
                .Include(o => o.Positions)
                .ThenInclude(o => o.Article)
                .ThenInclude(o => o.ArticleGroup)
                .Include(o => o.Customer)
                .ThenInclude(o => o.Address)
                .ThenInclude(o => o.Town)
                .ToListAsync();

            foreach(var entity in entities)
            {
                if(!entity.Customer.Firstname.Contains(searchString) && !entity.Customer.Lastname.Contains(searchString))
                {
                    entities.Remove(entity);
                }
            }
            return entities;
        }

        public async Task<Domain.Order.Order> Get(int id)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            Domain.Order.Order entity = await db.Orders
                .Include(o => o.Positions)
                .ThenInclude(o => o.Article)
                .ThenInclude(o => o.ArticleGroup)
                .Include(o => o.Customer)
                .ThenInclude(o => o.Address)
                .ThenInclude(o => o.Town)
                .FirstOrDefaultAsync(e => e.OrderId == id);
            return entity;
        }

        public async Task<IEnumerable<Domain.Order.Order>> GetAll()
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            List<Domain.Order.Order> entities = await db.Orders
                .Include(o => o.Positions)
                .ThenInclude(o => o.Article)
                .ThenInclude(o => o.ArticleGroup)
                .Include(o => o.Customer)
                .ThenInclude(o => o.Address)
                .ThenInclude(o => o.Town)
                .ToListAsync();
            return entities;
        }

        public async Task<ResponseDto<Domain.Order.Order>> Update(Domain.Order.Order entity)
        {
            ResponseDto<Domain.Order.Order> response = new ResponseDto<Domain.Order.Order>();
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var entry = await db.Orders.FirstOrDefaultAsync(e => e.OrderId == entity.OrderId);

                db.Entry(entry).CurrentValues.SetValues(entity);
                entity.Customer = await GetCustomer(entity.Customer, db);
                entity.Positions = await GetPositions(entity, db);

                response.NumberOfRows = await db.SaveChangesAsync();

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

        private async Task<Domain.Customer.Customer> GetCustomer(Domain.Customer.Customer customer, AppDbContext db)
        {
            var existingCustomer = await db.Customers
                .Include(e => e.Address)
                .ThenInclude(e => e.Town)
                .Include(e => e.Orders)
                .FirstOrDefaultAsync(e => e.CustomerId == customer.CustomerId);

            return existingCustomer;
        }

        private async Task<List<Domain.Position.Position>> GetPositions(Domain.Order.Order entity, AppDbContext db)
        {
            List<int> articleIds = new List<int>();

            foreach (var position in entity.Positions)
            {
                articleIds.Add(position.Article.ArticleId);
            }

            List<Domain.Position.Position> positionEntities = entity.Positions.ToList();

            int i = 0;
            foreach (var id in articleIds)
            {
                
                var article = await db.Articles
                    .Include(p => p.ArticleGroup)
                    .FirstOrDefaultAsync(a => a.ArticleId == id);

                positionEntities[i].Article = article;
                i++;
            }
            return positionEntities;
        }
    }
}
