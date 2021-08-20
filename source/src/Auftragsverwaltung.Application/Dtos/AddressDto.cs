using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.Application.Dtos
{
    public class AddressDto : AppDto
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string BuildingNr { get; set; }
        public int TownId { get; set; }
        public TownDto Town { get; set; } = default!;
        public CustomerDto Customer { get; set; }
    }
}
