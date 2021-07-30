using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Order;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace Auftragsverwaltung.Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IAppRepository<Order> _repository;
        private readonly IMapper _mapper;
        public OrderService(IAppRepository<Order> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<OrderDto> Create(OrderDto dto)
        {
            var entity = _mapper.Map<Order>(dto);

            var response = await _repository.Create(entity);
            var mappedResponse = _mapper.Map<OrderDto>(response);
            return mappedResponse;
        }
        
        public async Task<OrderDto> Delete(int id)
        {
            var response = await _repository.Delete(id);
            var mappedResponse = _mapper.Map<OrderDto>(response);
            return mappedResponse;
        }

        public async Task<OrderDto> Get(int id)
        {
            var data = await _repository.Get(id);
            var mappedData = _mapper.Map<OrderDto>(data);
            return mappedData;
        }

        public async Task<IEnumerable<OrderDto>> GetAll()
        {
            var data = await _repository.GetAll();
            var mappedData = data.Select(x => _mapper.Map<OrderDto>(x));
            return mappedData;
        }

        public async Task<OrderDto> Update(OrderDto dto)
        {
            var entity = _mapper.Map<Order>(dto);

            var response = await _repository.Update(entity);
            var mappedResponse = _mapper.Map<OrderDto>(response);
            return mappedResponse;
        }
    }
}
