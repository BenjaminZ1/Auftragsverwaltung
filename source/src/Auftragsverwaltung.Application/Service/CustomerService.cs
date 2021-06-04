using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Customer;

namespace Auftragsverwaltung.Application.Service
{
    class CustomerService : IAppService<CustomerDto, Customer>
    {
        public async Task<CustomerDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CustomerDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerDto> Create(Customer entity)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerDto> Update(int id, Customer entity)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerDto> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
