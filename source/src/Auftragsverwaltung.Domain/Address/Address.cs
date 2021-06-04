using Auftragsverwaltung.Domain.Common;
using System.Collections.Generic;

namespace Auftragsverwaltung.Domain.Address
{
    public class Address : EntityBase
    {
#nullable enable
        public int AddressId { get; set; }
        public string Street { get; set; } = default!;
        public string? BuildingNr { get; set; }
        public int TownId { get; set; }
        public virtual Town.Town Town { get; set; } = default!;
        public virtual ICollection<Customer.Customer> Customers { get; set; } = default!;
#nullable disable
    }
}
