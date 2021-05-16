using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Auftragsverwaltung.Domain.Common;

namespace Auftragsverwaltung.Domain
{
    public class Customer : EntityBase
    {
#nullable enable
        public int CustomerId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public string Email { get; set; }
        public string? Website { get; set; }
        public  Byte[] Password { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
#nullable disable

    }
}
