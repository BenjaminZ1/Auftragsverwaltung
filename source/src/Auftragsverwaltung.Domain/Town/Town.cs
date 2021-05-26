using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Domain.Common;

namespace Auftragsverwaltung.Domain
{
    public class Town : EntityBase
    {
#nullable enable
        public int TownId { get; set; }
        public string ZipCode { get; set; } = default!;
        public string Townname { get; set; } = default!;
        public virtual ICollection<Address> Addresses { get; set; } = default!;
#nullable disable
    }
}
