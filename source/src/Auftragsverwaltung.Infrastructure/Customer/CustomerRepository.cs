using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Auftragsverwaltung.Infrastructure.Customer
{
    public class CustomerRepository : IAppRepository<Domain.Customer>
    {
        private readonly AppDbContextFactory _dbContextFactory;

        public CustomerRepository(AppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Domain.Customer> Get(int id)
        {
            await using AppDbContext context = _dbContextFactory.CreateDbContext();
            Domain.Customer entity = await context.Customers.FirstOrDefaultAsync(e => e.CustomerId == id);

            return entity;
        }

        public async Task<IEnumerable<Domain.Customer>> GetAll()
        {
            await using AppDbContext context = _dbContextFactory.CreateDbContext();
            List<Domain.Customer> entities = await context.Customers.ToListAsync();

            return entities;
        }

        public async Task<Domain.Customer> Create(Domain.Customer entity)
        {
            await using AppDbContext context = _dbContextFactory.CreateDbContext();
            EntityEntry<Domain.Customer> createdEntity = await context.Customers.AddAsync(entity);
            await context.SaveChangesAsync();

            return createdEntity.Entity;
        }

        public async Task<Domain.Customer> Update(int id, Domain.Customer entity)
        {
            await using AppDbContext context = _dbContextFactory.CreateDbContext();
            entity.CustomerId = id;
            context.Customers.Update(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            await using AppDbContext context = _dbContextFactory.CreateDbContext();
            Domain.Customer entity = await context.Customers.FirstOrDefaultAsync(e => e.CustomerId == id);
            context.Customers.Remove(entity);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
