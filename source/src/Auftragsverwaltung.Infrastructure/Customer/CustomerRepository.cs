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
        private readonly AppDbContext _db;

        public CustomerRepository(AppDbContextFactory dbContextFactory)
        {
            _db = dbContextFactory.CreateDbContext();
        }

        public async Task<Domain.Customer> Get(int id)
        {
            Domain.Customer entity = await _db.Customers.FirstOrDefaultAsync(e => e.CustomerId == id);
            return entity;
        }

        public async Task<IEnumerable<Domain.Customer>> GetAll()
        {
            List<Domain.Customer> entities = await _db.Customers.ToListAsync();
            return entities;
        }

        public async Task<Domain.Customer> Create(Domain.Customer entity)
        {
            entity.Address = await FindOrAddNewAddress(entity.Address);
            EntityEntry<Domain.Customer> createdEntity = await _db.Customers.AddAsync(entity);
            await _db.SaveChangesAsync();

            return createdEntity.Entity;
        }

        public async Task<Domain.Customer> Update(int id, Domain.Customer entity)
        {
            entity.CustomerId = id;
            _db.Customers.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            Domain.Customer entity = await _db.Customers.FirstOrDefaultAsync(e => e.CustomerId == id);
            _db.Customers.Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        private async Task<Domain.Town> FindOrAddNewTown(Domain.Town town)
        {
            Domain.Town foundTown = await _db.Towns.FirstOrDefaultAsync(e =>
                e.Townname == town.Townname &&
                e.ZipCode == town.ZipCode);

            return foundTown ?? town;
        }

        private async Task<Domain.Address> FindOrAddNewAddress(Domain.Address address)
        {
            Domain.Address foundAddress = await _db.Addresses
                .Include(e => e.Town)
                .FirstOrDefaultAsync(e =>
                e.BuildingNr == address.BuildingNr &&
                e.Street == address.Street);

            return foundAddress ?? address;
        }
    }
}
