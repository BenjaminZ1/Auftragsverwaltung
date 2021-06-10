using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Auftragsverwaltung.Application.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IAppRepository<Customer> _repository;
        public CustomerService(IAppRepository<Customer> repository)
        {
            _repository = repository;
        }
        public async Task<CustomerDto> Get(int id)
        {
            var data = await _repository.Get(id);
            var mappedData = new CustomerDto(data);
            return mappedData;
        }

        public async Task<IEnumerable<CustomerDto>> GetAll()
        {
            var data = await _repository.GetAll();
            var mappedData = data.Select(x => new CustomerDto(x));
            return mappedData;
        }

        public async Task<CustomerDto> Create(Customer entity)
        {
            var response = await _repository.Create(entity);
            var mappedResponse = new CustomerDto(response);
            return mappedResponse;
        }

        public async Task<CustomerDto> Update(int id, Customer entity)
        {
            var response = await _repository.Update(id, entity);
            var mappedResponse = new CustomerDto(response);
            return mappedResponse;
        }

        public async Task<CustomerDto> Delete(int id)
        {
            var response = await _repository.Delete(id);
            var mappedResponse = new CustomerDto(response);
            return mappedResponse;
        }
    }
}
