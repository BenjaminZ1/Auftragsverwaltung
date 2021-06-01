using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Domain.Common;

namespace Auftragsverwaltung.Domain
{
    public class Order : EntityBase
    {
#nullable enable
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = default!;
        public virtual ICollection<Position> Positions { get; set; } = default!;
#nullable disable
    }
}
