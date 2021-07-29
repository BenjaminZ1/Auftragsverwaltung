using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.Application.Dtos
{
    public class TownDto : AppDto
    {
        public int TownId { get; set; }
        public string ZipCode { get; set; } = default!;
        public string Townname { get; set; } = default!;
        public virtual ICollection<AddressDto> Addresses { get; set; }
    }
}
