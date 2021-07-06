using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Order;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IAppRepository<Order> _repository;
        public OrderService(IAppRepository<Order> repository)
        {
            _repository = repository;
        }
        public async Task<OrderDto> Create(OrderDto dto)
        {
            var entity = ConvertToEntity(dto);

            var response = await _repository.Create(entity);
            var mappedResponse = new OrderDto(response);
            return mappedResponse;
        }

        internal Order ConvertToEntity(OrderDto orderDto)
        {
            return new Order()
            {
                Date = orderDto.Date,
                Customer = orderDto.Customer,
                Positions = orderDto.Positions
            };
        }

        public async Task<OrderDto> Delete(int id)
        {
            var response = await _repository.Delete(id);
            var mappedResponse = new OrderDto(response);
            return mappedResponse;
        }

        public async Task<OrderDto> Get(int id)
        {
            var data = await _repository.Get(id);
            var mappedData = new OrderDto(data);
            return mappedData;
        }

        public async Task<IEnumerable<OrderDto>> GetAll()
        {
            var data = await _repository.GetAll();
            var mappedData = data.Select(x => new OrderDto(x));
            return mappedData;
        }

        public async Task<OrderDto> Update(OrderDto dto)
        {
            var entity = ConvertToEntity(dto);

            var response = await _repository.Update(entity);
            var mappedResponse = new OrderDto(response);
            return mappedResponse;
        }
    }
}
