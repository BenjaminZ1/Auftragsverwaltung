using Auftragsverwaltung.Domain.Common;
using System.Collections.Generic;

namespace Auftragsverwaltung.Domain.Town
{
    public class Town : EntityBase
    {
#nullable enable
        public int TownId { get; set; }
        public string ZipCode { get; set; } = default!;
        public string Townname { get; set; } = default!;
        public virtual ICollection<Address.Address> Addresses { get; set; } = default!;
#nullable disable
    }
}
