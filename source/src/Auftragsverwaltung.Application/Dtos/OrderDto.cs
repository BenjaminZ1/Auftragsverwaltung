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
        public  CustomerDto Customer { get; set; }
        public  ICollection<PositionDto> Positions { get; set; }
        public ResponseDto<Order> Response { get; set; }

        public OrderDto()
        {
            Customer = new CustomerDto();
            Positions = new List<PositionDto>();
        }
    }
}
