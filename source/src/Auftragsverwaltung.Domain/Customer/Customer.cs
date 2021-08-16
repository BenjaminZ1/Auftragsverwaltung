using Auftragsverwaltung.Domain.Common;
using System;
using System.Collections.Generic;

namespace Auftragsverwaltung.Domain.Customer
{
    public class Customer : EntityBase
    {
#nullable enable
        public int CustomerId { get; set; }
        public string CustomerNumber { get; set; } = default!;
        public string Firstname { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public int AddressId { get; set; }
        public virtual Address.Address Address { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? Website { get; set; }
        public Byte[] Password { get; set; } = default!;
        public virtual ICollection<Order.Order> Orders { get; set; } = default!;
#nullable disable
    }
}
