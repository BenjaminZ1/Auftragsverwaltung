using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using Auftragsverwaltung.Domain.Order;
using Auftragsverwaltung.Domain.Position;
using System;
using System.Collections.Generic;

namespace Auftragsverwaltung.Application.Dtos
{
    public class OrderDto : AppDto
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public  CustomerDto Customer { get; set; } = default!;
        public  ICollection<PositionDto> Positions { get; set; } = default!;
        public ResponseDto<Order> Response { get; set; }

        public OrderDto()
        {
            Customer = new CustomerDto();
            Positions = new List<PositionDto>();
        }

        public OrderDto(Order order)
        {
            OrderId = order.OrderId;
            Date = order.Date;
            CustomerId = order.CustomerId;
            Customer = new CustomerDto(order.Customer);
            //Positions = order.Positions;
        }

        public OrderDto(ResponseDto<Order> response)
        {
            Response = response;
            if (response.Entity != null)
            {
                Response = response;
                OrderId = response.Entity.OrderId;
                Date = response.Entity.Date;
                CustomerId = response.Entity.CustomerId;
                Customer = new CustomerDto(response.Entity.Customer);
                //Positions = response.Entity.Positions;
            }
            response.Entity = null;
        }
    }
}
