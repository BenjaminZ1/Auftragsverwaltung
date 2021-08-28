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

                //if (await IsNewTownRequired(entity.Addresses.Town))
                //{
                //    var newTown = new Domain.Town.Town()
                //    {
                //        Townname = entity.Addresses.Town.Townname,
                //        ZipCode = entity.Addresses.Town.ZipCode
                //    };
                //    entity.Addresses.Town = newTown;
                //    db.Towns.Add(newTown);

                //    await db.SaveChangesAsync();
                //    entity.Addresses.TownId = newTown.TownId;
                //}
                //else
                //{
                //    entity.Addresses.Town = await FindOrAddNewTown(entity.Addresses.Town, db);
                //}

                db.Customers.Update(entity);
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
                //int townId = entity.Addresses.TownId;

                //if (!(await IsTownInUse(townId)))
                //{
                //    db.RemoveRange(entity.Addresses.Town);
                //    db.RemoveRange(entity.Addresses);
                //}
                //else
                //{
                //    db.RemoveRange(entity.Addresses);
                //}

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
