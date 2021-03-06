using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Infrastructure.Customer
{
    public class CustomerRepository : IAppRepository<Domain.Customer.Customer>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public CustomerRepository(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<Domain.Customer.Customer> Get(int id)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            Domain.Customer.Customer entity = await db.Customers
            .Include(e => e.Addresses)
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
                .Include(e => e.Addresses)
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
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetService<AppDbContext>();

                entity.Addresses.First().Town = await FindOrAddNewTown(entity.Addresses.First().Town, db);
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
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetService<AppDbContext>();

                var entry = await db.Customers
                    .Include(e => e.Addresses)
                    .ThenInclude(e => e.Town)
                    .FirstOrDefaultAsync(e => e.CustomerId == entity.CustomerId);

                var currentValidAddressDb = entry.Addresses.First(a =>
                    a.ValidUntil == DateTime.MaxValue);
                var newValidAddress = entity.Addresses.First(a =>
                    a.ValidUntil == DateTime.MaxValue);

                entity.Addresses = entry.Addresses;

                if (currentValidAddressDb.BuildingNr != newValidAddress.BuildingNr ||
                    currentValidAddressDb.Street != newValidAddress.Street || !currentValidAddressDb.Town.Equals(newValidAddress.Town))
                {
                    newValidAddress.AddressId = 0;

                    entity.Addresses.Add(newValidAddress);
                    currentValidAddressDb.ValidUntil = DateTime.Now;

                    if (await IsNewTownRequired(newValidAddress.Town))
                    {
                        var newTown = new Domain.Town.Town()
                        {
                            Townname = newValidAddress.Town.Townname,
                            ZipCode = newValidAddress.Town.ZipCode
                        };
                        newValidAddress.Town = newTown;
                        db.Towns.Add(newTown);

                        await db.SaveChangesAsync();
                        newValidAddress.TownId = newTown.TownId;
                    }
                    else
                    {
                        newValidAddress.Town = await FindOrAddNewTown(newValidAddress.Town, db);
                    }
                }
                else
                {
                    if (await IsNewTownRequired(currentValidAddressDb.Town))
                    {
                        var newTown = new Domain.Town.Town()
                        {
                            Townname = currentValidAddressDb.Town.Townname,
                            ZipCode = currentValidAddressDb.Town.ZipCode
                        };
                        currentValidAddressDb.Town = newTown;
                        db.Towns.Add(newTown);

                        await db.SaveChangesAsync();
                        currentValidAddressDb.TownId = newTown.TownId;
                    }
                    else
                    {
                        currentValidAddressDb.Town = await FindOrAddNewTown(currentValidAddressDb.Town, db);
                    }
                }

                db.Entry(entry).CurrentValues.SetValues(entity);
                response.NumberOfRows = await db.SaveChangesAsync();

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
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetService<AppDbContext>();

                Domain.Customer.Customer entity = await this.Get(id);

                foreach (var address in entity.Addresses)
                {
                    if (!(await IsTownInUse(address.Town.TownId)))
                    {
                        db.RemoveRange(address.Town);
                        db.RemoveRange(address);
                    }
                    else
                    {
                        db.RemoveRange(address);
                    }
                }

                db.Customers.Remove(entity);
                response.NumberOfRows = await db.SaveChangesAsync();

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
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetService<AppDbContext>();
            List<Domain.Customer.Customer> entities = await db.Customers
                .Include(e => e.Addresses)
                .ThenInclude(e => e.Town)
                .Include(e => e.Orders)
                .Where(e => e.Firstname.Contains(searchString) || e.Lastname.Contains(searchString))
                .ToListAsync();

            return entities;
        }

        private async Task<bool> IsTownInUse(int townId)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetService<AppDbContext>();
            Domain.Town.Town foundTown = await db.Towns
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
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetService<AppDbContext>();
            Domain.Town.Town foundTown = await db.Towns
                .Include(e => e.Addresses)
                .FirstOrDefaultAsync(e =>
                    e.Townname == town.Townname && e.ZipCode == town.ZipCode);

            if (foundTown == null)
                return true;
            return false;
        }


    }
}
