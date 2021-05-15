using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Domain.Common;

namespace Auftragsverwaltung.Domain
{
    public class Town : EntityBase
    {
#nullable enable
        public int TownId { get; set; }
        public string ZipCode { get; set; }
        public string Townname { get; set; }
        public virtual ICollection<Address> Adresses { get; set; }
#nullable disable
    }
}
