using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using AutoMapper;

namespace Auftragsverwaltung.Application.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IAppRepository<Customer> _repository;
        private readonly IMapper _mapper;
        public CustomerService(IAppRepository<Customer> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Get(int id)
        {
            var data = await _repository.Get(id);
            var mappedData = _mapper.Map<CustomerDto>(data);
            return mappedData;
        }

        public async Task<IEnumerable<CustomerDto>> GetAll()
        {
            var data = await _repository.GetAll();
            var mappedData = data.Select(x => _mapper.Map<CustomerDto>(x));
            return mappedData;
        }

        public async Task<CustomerDto> Create(CustomerDto dto)
        {
            var entity = _mapper.Map<Customer>(dto);

            var response = await _repository.Create(entity);
            var mappedResponse = _mapper.Map<CustomerDto>(response);
            return mappedResponse;
        }

        public async Task<CustomerDto> Update(CustomerDto dto)
        {
            var entity = _mapper.Map<Customer>(dto);

            var response = await _repository.Update(entity);
            var mappedResponse = _mapper.Map<CustomerDto>(response);
            return mappedResponse;
        }

        public async Task<CustomerDto> Delete(int id)
        {
            var response = await _repository.Delete(id);
            var mappedResponse = _mapper.Map<CustomerDto>(response);
            return mappedResponse;
        }

        internal static  Customer ConvertToEntity(CustomerDto customerDto)
        {
            return new Customer()
            {
                CustomerId = customerDto.CustomerId,
                Firstname = customerDto.Firstname,
                Lastname = customerDto.Lastname,
                AddressId = customerDto.AddressId,
                //Address = customerDto.Address,
                Email = customerDto.Email,
                Website = customerDto.Website,
                Password = new byte[64],
                //Orders = customerDto.Orders
            };
        }
    }
}
