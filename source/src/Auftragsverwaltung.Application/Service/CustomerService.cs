using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;

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

        public async Task<CustomerDto> Create(CustomerDto dto)
        {
            var entity = ConvertToEntity(dto);

            var response = await _repository.Create(entity);
            var mappedResponse = new CustomerDto(response);
            return mappedResponse;
        }

        public async Task<CustomerDto> Update(CustomerDto dto)
        {
            var entity = ConvertToEntity(dto);

            var response = await _repository.Update(entity);
            var mappedResponse = new CustomerDto(response);
            return mappedResponse;
        }

        public async Task<CustomerDto> Delete(int id)
        {
            var response = await _repository.Delete(id);
            var mappedResponse = new CustomerDto(response);
            return mappedResponse;
        }

        internal Customer ConvertToEntity(CustomerDto customerDto)
        {
            return new Customer()
            {
                CustomerId = customerDto.CustomerId,
                Firstname = customerDto.Firstname,
                Lastname = customerDto.Lastname,
                AddressId = customerDto.AddressId,
                Address = customerDto.Address,
                Email = customerDto.Email,
                Website = customerDto.Website,
                Password = new byte[64],
                Orders = customerDto.Orders
            };
        }
    }
}
