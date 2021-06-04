using Auftragsverwaltung.Domain.Common;
using System;
using System.Collections.Generic;

namespace Auftragsverwaltung.Domain.Order
{
    public class Order : EntityBase
    {
#nullable enable
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer.Customer Customer { get; set; } = default!;
        public virtual ICollection<Position.Position> Positions { get; set; } = default!;
#nullable disable
    }
}
