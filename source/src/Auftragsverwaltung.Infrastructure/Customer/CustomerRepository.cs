using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auftragsverwaltung.Application.Dtos;
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
            Domain.Customer entity = await _db.Customers
                .Include(e => e.Address)
                .Include(e => e.Orders)
                .FirstOrDefaultAsync(e => e.CustomerId == id);
            return entity;
        }

        public async Task<IEnumerable<Domain.Customer>> GetAll()
        {
            List<Domain.Customer> entities = await _db.Customers
                .Include(e => e.Address)
                .Include(e => e.Orders)
                .ToListAsync();
            return entities;
        }

        public async Task<ResponseDto<Domain.Customer>> Create(Domain.Customer entity)
        {
            ResponseDto<Domain.Customer> response = new ResponseDto<Domain.Customer>();
            try
            {
                entity.Address = await FindOrAddNewAddress(entity.Address);
                EntityEntry<Domain.Customer> createdEntity = await _db.Customers.AddAsync(entity);
                response.NumberOfRows = await _db.SaveChangesAsync();

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

        public async Task<ResponseDto<Domain.Customer>> Update(int id, Domain.Customer entity)
        {
            ResponseDto<Domain.Customer> response = new ResponseDto<Domain.Customer>();
            try
            {
                entity.CustomerId = id;
                _db.Customers.Update(entity);
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

        public async Task<ResponseDto<Domain.Customer>> Delete(int id)
        {
            ResponseDto<Domain.Customer> response = new ResponseDto<Domain.Customer>();
            try
            {
                Domain.Customer entity = await this.Get(id);
                int addressId = entity.Address.AddressId;
                int townId = entity.Address.TownId;

                if (!(IsAddressInUse(addressId).Result))
                {
                    if (!(IsTownInUse(townId).Result))
                    {
                        _db.RemoveRange(entity.Address.Town);
                        _db.RemoveRange(entity.Address);
                    }
                    else
                        _db.RemoveRange(entity.Address);
                }

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

        private async Task<bool> IsAddressInUse(int addressId)
        {
            Domain.Address foundAddress = await _db.Addresses
                .Include(e => e.Customers)
                .FirstOrDefaultAsync(e =>
                    e.AddressId == addressId);

            if (foundAddress.Customers.Count <= 1)
                return false;
            return true;
        }

        private async Task<bool> IsTownInUse(int townId)
        {
            Domain.Town foundTown = await _db.Towns
                .Include(e => e.Addresses)
                .FirstOrDefaultAsync(e =>
                    e.TownId == townId);

            if (foundTown.Addresses.Count <= 1)
                return false;
            return true;
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
