using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Domain.Common;

namespace Auftragsverwaltung.Domain
{
    public class Customer : EntityBase
    {
#nullable enable
        public int CustomerId { get; set; }
        public string Firstname { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public int AddressId { get; set; }
        public virtual Address Address { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? Website { get; set; }
        public  Byte[] Password { get; set; } = default!;
        public virtual ICollection<Order> Orders { get; set; } = default!;
#nullable disable

    }
}
