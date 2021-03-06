using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.ArticleGroup;
using Auftragsverwaltung.Domain.Order;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository repository, IMapper mapper)
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
            return SetValidAddressAtOrderDate(mappedData);
        }

        public async Task<OrderDto> Update(OrderDto dto)
        {
            var entity = _mapper.Map<Order>(dto);

            var response = await _repository.Update(entity);
            var mappedResponse = _mapper.Map<OrderDto>(response);
            return mappedResponse;
        }

        public async Task<IEnumerable<OrderDto>> Search(string searchString)
        {
            var response = await _repository.Search(searchString);
            var mappedResponse = response.Select(x => _mapper.Map<OrderDto>(x));
            return mappedResponse;
        }

        public DataTable GetQuarterData()
        {
            var response = _repository.GetQuarterDataTable();
            return response;
        }

        private IEnumerable<OrderDto> SetValidAddressAtOrderDate(IEnumerable<OrderDto> orderDtos)
        {
            var orderDtoList = orderDtos.ToList();
            foreach (var order in orderDtoList)
            {
                order.Customer.ValidAddress =
                    order.Customer.Addresses.FirstOrDefault(a => a.ValidFrom <= order.Date && a.ValidUntil >= order.Date);
            }

            return orderDtoList;
        }

        public async Task<IEnumerable<OrderOverviewDto>> GetOrderOverview()
        {
            var data = await GetAll();
            var orderOverview = data
                .OrderBy(d => d.Customer.Firstname)
                .ThenBy(d => d.Customer.Lastname)
                .ThenBy(d => d.Date)
                .Select(d => new OrderOverviewDto()
                {
                    CustomerNumber = d.Customer.CustomerNumber,
                    Name = d.Customer.Firstname + " " + d.Customer.Lastname,
                    Street = d.Customer.ValidAddress.Street,
                    BuildingNr = d.Customer.ValidAddress.BuildingNr,
                    ZipCode = d.Customer.ValidAddress.Town.ZipCode,
                    Town = d.Customer.ValidAddress.Town.Townname,
                    OrderDate = d.Date,
                    OrderId = d.OrderId,
                    Netto = decimal.Round(d.Positions.Select(p => p.Amount * p.Article.Price / 107.7m * 100).Sum(), 2, MidpointRounding.AwayFromZero),
                    Brutto = decimal.Round(d.Positions.Select(p => p.Amount * p.Article.Price).Sum(), 2, MidpointRounding.AwayFromZero)
                });

            return orderOverview;
        }
    }
}
