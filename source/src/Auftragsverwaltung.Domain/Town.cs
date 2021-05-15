using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.Domain
{
    public class Town
    {
#nullable enable
        public int TownId { get; set; }
        public string ZipCode { get; set; }
        public string Townname { get; set; }
        public virtual ICollection<Adress> Adresses { get; set; }
#nullable disable
    }
}
