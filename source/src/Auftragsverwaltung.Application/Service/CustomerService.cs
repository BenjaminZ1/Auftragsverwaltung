using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;

namespace Auftragsverwaltung.Application.Service
{
    public class CustomerService : IAppService<CustomerDto>
    {
        private readonly IAppRepository<Customer> _db;

        public CustomerService(IAppRepository<Customer> db)
        {
            _db = db;
        }

        public async Task<CustomerDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CustomerDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerDto> Create(CustomerDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerDto> Update(int id, CustomerDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerDto> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
