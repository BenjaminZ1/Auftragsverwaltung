using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace Auftragsverwaltung.Application.Dtos
{
    public class AddressDto : AppDto
    {
        [JsonIgnore]
        [XmlIgnore]
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string BuildingNr { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public int TownId { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public int CustomerId { get; set; }
        public TownDto Town { get; set; } = default!;
        [JsonIgnore]
        [XmlIgnore]
        public CustomerDto Customer { get; set; }
    }
}
