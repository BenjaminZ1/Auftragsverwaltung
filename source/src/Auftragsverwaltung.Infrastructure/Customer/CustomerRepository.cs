using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Infrastructure.Customer
{
    public class CustomerRepository : IAppRepository<Domain.Customer.Customer>
    {
        private readonly AppDbContext _db;

        public CustomerRepository(AppDbContextFactory dbContextFactory)
        {
            _db = dbContextFactory.CreateDbContext();
        }

        public async Task<Domain.Customer.Customer> Get(int id)
        {
            Domain.Customer.Customer entity = await _db.Customers
                .Include(e => e.Address)
                .ThenInclude(e => e.Town)
                .Include(e => e.Orders)
                .FirstOrDefaultAsync(e => e.CustomerId == id);
            return entity;
        }

        public async Task<IEnumerable<Domain.Customer.Customer>> GetAll()
        {
            List<Domain.Customer.Customer> entities = await _db.Customers
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
                entity.Address = await FindOrAddNewAddress(entity.Address);
                EntityEntry<Domain.Customer.Customer> createdEntity = await _db.Customers.AddAsync(entity);
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

        public async Task<ResponseDto<Domain.Customer.Customer>> Update(Domain.Customer.Customer entity)
        {
            ResponseDto<Domain.Customer.Customer> response = new ResponseDto<Domain.Customer.Customer>();
            try
            {
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

        public async Task<ResponseDto<Domain.Customer.Customer>> Delete(int id)
        {
            ResponseDto<Domain.Customer.Customer> response = new ResponseDto<Domain.Customer.Customer>();
            try
            {
                Domain.Customer.Customer entity = await this.Get(id);
                int addressId = entity.Address.AddressId;
                int townId = entity.Address.TownId;

                if (!(await IsAddressInUse(addressId)))
                {
                    if (!(await IsTownInUse(townId)))
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

        public async Task<IEnumerable<Domain.Customer.Customer>> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> IsAddressInUse(int addressId)
        {
            Domain.Address.Address foundAddress = await _db.Addresses
                .Include(e => e.Customers)
                .FirstOrDefaultAsync(e =>
                    e.AddressId == addressId);

            if (foundAddress.Customers.Count <= 1)
                return false;
            return true;
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

        private async Task<Domain.Address.Address> FindOrAddNewAddress(Domain.Address.Address address)
        {
            Domain.Address.Address foundAddress = await _db.Addresses
                .Include(e => e.Town)
                .FirstOrDefaultAsync(e =>
                e.BuildingNr == address.BuildingNr &&
                e.Street == address.Street);

            return foundAddress ?? address;
        }
    }
}
