﻿using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using System.Collections.Generic;
using System.Security;

namespace Auftragsverwaltung.Application.Dtos
{
    public class CustomerDto : AppDto
    {
        public int CustomerId { get; set; }
        public string CustomerNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public ICollection<AddressDto> Addresses { get; set; }
        public AddressDto ValidAddress { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public SecureString Password { get; set; }
        public ICollection<OrderDto> Orders { get; set; }
        public ResponseDto<Customer> Response { get; set; }

        public CustomerDto()
        {
            Addresses = new List<AddressDto>();
            Addresses.Add(new AddressDto() { Town = new TownDto() });
        }
    }
}
