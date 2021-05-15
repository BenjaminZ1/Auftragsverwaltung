using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Domain.Common;

namespace Auftragsverwaltung.Domain
{
    public class Adress : EntityBase
    {
#nullable enable
        public int AdressId { get; set; }
        public string? BuildingNr { get; set; }
        public virtual Town Town { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
#nullable disable
    }
}
