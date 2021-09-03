using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Auftragsverwaltung.Application.Dtos
{
    public class TownDto : AppDto
    {
        [JsonIgnore]
        [XmlIgnore]
        public int TownId { get; set; }
        public string ZipCode { get; set; }
        public string Townname { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public ICollection<AddressDto> Addresses { get; set; }
    }
}
