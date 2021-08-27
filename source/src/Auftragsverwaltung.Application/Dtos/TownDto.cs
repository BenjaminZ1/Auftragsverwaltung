using System.Collections.Generic;

namespace Auftragsverwaltung.Application.Dtos
{
    public class TownDto : AppDto
    {
        public int TownId { get; set; }
        public string ZipCode { get; set; }
        public string Townname { get; set; }
        public ICollection<AddressDto> Addresses { get; set; }
    }
}
