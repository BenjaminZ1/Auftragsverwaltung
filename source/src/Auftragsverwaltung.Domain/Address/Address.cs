using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Domain.Common;

namespace Auftragsverwaltung.Domain
{
    public class Address : EntityBase
    {
#nullable enable
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string? BuildingNr { get; set; }
        public int TownId { get; set; }
        public virtual Town Town { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
#nullable disable
    }
}
