using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Address;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Application.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IAppRepository<Customer> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<CustomerDto> _validator;
        private readonly ISerializer<CustomerDto> _serializer;

        public CustomerService(IAppRepository<Customer> repository, IMapper mapper,
            IValidator<CustomerDto> validator, ISerializer<CustomerDto> serializer)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
            _serializer = serializer;
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
            var result = _validator.Validate(dto);

            if (!result.IsValid)
            {
                StringBuilder errorBld = new StringBuilder();
                foreach (var failure in result.Errors)
                {
                    errorBld.Append($"{failure.PropertyName}: {failure.ErrorMessage } \n");
                }

                return new CustomerDto()
                {
                    Response = new ResponseDto<Customer>() { Flag = false, Message = errorBld.ToString() }
                };
            }

            dto.ValidAddress.ValidFrom = DateTime.Now;
            dto.ValidAddress.ValidUntil = DateTime.MaxValue;

            var entity = _mapper.Map<Customer>(dto);
            entity.Addresses.Add(_mapper.Map<Address>(dto.ValidAddress));

            var response = await _repository.Create(entity);
            var mappedResponse = _mapper.Map<CustomerDto>(response);
            return mappedResponse;
        }

        public async Task<CustomerDto> Update(CustomerDto dto)
        {
            var result = _validator.Validate(dto);

            if (!result.IsValid)
            {
                StringBuilder errorBld = new StringBuilder();
                foreach (var failure in result.Errors)
                {
                    errorBld.Append($"{failure.PropertyName}: {failure.ErrorMessage } \n");
                }
                return new CustomerDto()
                {
                    Response = new ResponseDto<Customer>() { Flag = false, Message = errorBld.ToString() }
                };
            }

            dto.ValidAddress.ValidFrom = DateTime.Now;
            dto.ValidAddress.ValidUntil = DateTime.MaxValue;

            var entity = _mapper.Map<Customer>(dto);
            entity.Addresses.Add(_mapper.Map<Address>(dto.ValidAddress));

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

        public async Task<IEnumerable<CustomerDto>> Search(string searchString)
        {
            var response = await _repository.Search(searchString);
            var mappedResponse = response.Select(x => _mapper.Map<CustomerDto>(x));
            return mappedResponse;
        }

        public async Task Serialize(CustomerDto customer, string filename)
        {
            await Task.Run(() => _serializer.Serialize(customer, filename));
        }

        public async Task<CustomerDto> Deserialize(string filename)
        {
            return await Task.Run(() => _serializer.Deserialize(filename));
        }
    }
}
