using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.Domain
{
    class Adress
    {
#nullable enable
        public int AdressId { get; set; }
        public string? BuildingNr { get; set; }
        public virtual Town Town { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
#nullable disable
    }
}
