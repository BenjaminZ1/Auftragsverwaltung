using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using Auftragsverwaltung.Domain.Order;
using System;
using System.Collections.Generic;
using System.Text;
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

        public Task<OrderDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OrderDto> Update(OrderDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
