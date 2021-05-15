using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.Domain
{
    public class Order
    {
#nullable enable
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public virtual Customer Customer { get; set; }
#nullable disable
    }
}
