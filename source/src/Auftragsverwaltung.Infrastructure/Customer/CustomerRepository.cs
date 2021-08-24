using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Auftragsverwaltung.Infrastructure.Customer
{
    public class CustomerRepository : IAppRepository<Domain.Customer.Customer>
    {
        private readonly AppDbContext _db;
        private readonly IServiceScopeFactory _scopeFactory;

        public CustomerRepository(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        public CustomerRepository(AppDbContextFactory dbContextFactory)
        {
            _db = dbContextFactory.CreateDbContext();
        }

        public CustomerRepository(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<Domain.Customer.Customer> Get(int id)
        {
            using var scope = _scopeFactory.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            Domain.Customer.Customer entity = await db.Customers
            .Include(e => e.Address)
            .ThenInclude(e => e.Town)
            .Include(e => e.Orders)
            .FirstOrDefaultAsync(e => e.CustomerId == id);
        return entity;

        }

        public async Task<IEnumerable<Domain.Customer.Customer>> GetAll()
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            List<Domain.Customer.Customer> entities = await db.Customers
                .Include(e => e.Address)
                .ThenInclude(e => e.Town)
                .Include(e => e.Orders)
                .ToListAsync();
            return entities;
        }

        public async Task<ResponseDto<Domain.Customer.Customer>> Create(Domain.Customer.Customer entity)
        {
            ResponseDto<Domain.Customer.Customer> response = new ResponseDto<Domain.Customer.Customer>();
            try
            {
                if (entity.Address.Street == "Hauptstrasse")
                    Thread.Sleep(1);
                using var scope = _scopeFactory.CreateScope();

                var db = scope.ServiceProvider.GetService<AppDbContext>();

                entity.Address.Town = await FindOrAddNewTown(entity.Address.Town, db);
                EntityEntry<Domain.Customer.Customer> createdEntity = await db.Customers.AddAsync(entity);
                response.NumberOfRows = await db.SaveChangesAsync();

                response.Entity = createdEntity.Entity;
                response.Flag = true;
                response.Message = "Has been added.";
                response.Id = createdEntity.Entity.CustomerId;

            }
            catch (Exception e)
            {
                response.Flag = false;
                response.Message = e.ToString();
            }

            return response;
        }

        public async Task<ResponseDto<Domain.Customer.Customer>> Update(Domain.Customer.Customer entity)
        {
            ResponseDto<Domain.Customer.Customer> response = new ResponseDto<Domain.Customer.Customer>();
            try
            {
                var entry = await this.Get(entity.CustomerId);

                if (await IsNewTownRequired(entity.Address.Town))
                {
                    var newTown = new Domain.Town.Town()
                    {
                        Townname = entity.Address.Town.Townname,
                        ZipCode = entity.Address.Town.ZipCode
                    };
                    entity.Address.Town = newTown;
                    _db.Towns.Add(newTown);

                    await _db.SaveChangesAsync();
                    entity.Address.TownId = newTown.TownId;
                }

                _db.Entry(entry).CurrentValues.SetValues(entity);
                entry.Address.BuildingNr = entity.Address.BuildingNr;
                entry.Address.Street = entity.Address.Street;
                entry.Address.Town = await FindOrAddNewTown(entity.Address.Town, _db);

                response.NumberOfRows = await _db.SaveChangesAsync();

                response.Entity = entity;
                response.Flag = true;
                response.Message = "Has been updated.";
                response.Id = entity.CustomerId;
            }
            catch (Exception e)
            {
                response.Flag = false;
                response.Message = e.ToString();
            }

            return response;
        }

        public async Task<ResponseDto<Domain.Customer.Customer>> Delete(int id)
        {
            ResponseDto<Domain.Customer.Customer> response = new ResponseDto<Domain.Customer.Customer>();
            try
            {
                Domain.Customer.Customer entity = await this.Get(id);
                int addressId = entity.Address.AddressId;
                int townId = entity.Address.TownId;

                //if (!(await IsAddressInUse(addressId)))
                //{
                    if (!(await IsTownInUse(townId)))
                    {
                        _db.RemoveRange(entity.Address.Town);
                        _db.RemoveRange(entity.Address);
                    }
                    else
                        _db.RemoveRange(entity.Address);
                //}

                _db.Customers.Remove(entity);
                response.NumberOfRows = await _db.SaveChangesAsync();

                response.Entity = entity;
                response.Flag = true;
                response.Message = "Has been deleted.";
                response.Id = entity.CustomerId;

            }
            catch (Exception e)
            {
                response.Flag = false;
                response.Message = e.ToString();
            }

            return response;
        }

        public async Task<IEnumerable<Domain.Customer.Customer>> Search(string searchString)
        {
            List<Domain.Customer.Customer> entities = await _db.Customers
                .Include(e => e.Address)
                .ThenInclude(e => e.Town)
                .Include(e => e.Orders)
                .ToListAsync();

            foreach(var entity in entities)
            {
                if(!entity.Firstname.Contains(searchString) && !entity.Lastname.Contains(searchString))
                {
                    entities.Remove(entity);
                }
            }
            return entities;
        }

        private async Task<bool> IsTownInUse(int townId)
        {
            Domain.Town.Town foundTown = await _db.Towns
                .Include(e => e.Addresses)
                .FirstOrDefaultAsync(e =>
                    e.TownId == townId);

            if (foundTown.Addresses.Count <= 1)
                return false;
            return true;
        }

        private async Task<Domain.Town.Town> FindOrAddNewTown(Domain.Town.Town town, AppDbContext db)
        {
            Domain.Town.Town foundTown = await db.Towns
                .FirstOrDefaultAsync(e =>
                    e.Townname == town.Townname &&
                    e.ZipCode == town.ZipCode);

            return foundTown ?? town;
        }

        private async Task<bool> IsNewTownRequired(Domain.Town.Town town)
        {
            Domain.Town.Town foundTown = await _db.Towns
                .Include(e => e.Addresses)
                .FirstOrDefaultAsync(e =>
                    e.Townname == town.Townname && e.ZipCode == town.ZipCode);

            if (foundTown == null)
                return true;
            return false;
        }
    }
}
