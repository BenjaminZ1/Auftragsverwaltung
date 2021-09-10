using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security;
using System.Xml.Serialization;

namespace Auftragsverwaltung.Application.Dtos
{
    [Serializable]
    public class CustomerDto : AppDto
    {
        [JsonIgnore]
        [XmlIgnore]
        public int CustomerId { get; set; }
        public string CustomerNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public ICollection<AddressDto> Addresses { get; set; }
        [XmlElement(ElementName = "Address")]
        public AddressDto ValidAddress { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public SecureString Password { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public ICollection<OrderDto> Orders { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public ResponseDto<Customer> Response { get; set; }

        public CustomerDto()
        {
            Addresses = new List<AddressDto>();
            ValidAddress = new AddressDto()
            {
                Town = new TownDto()
            };
        }
    }
}
