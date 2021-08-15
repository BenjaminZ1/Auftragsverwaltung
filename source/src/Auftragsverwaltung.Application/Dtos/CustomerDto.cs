﻿using System;
using System.Collections.Generic;
using Auftragsverwaltung.Domain.Address;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using Auftragsverwaltung.Domain.Order;
using Auftragsverwaltung.Domain.Town;

namespace Auftragsverwaltung.Application.Dtos
{
    public class CustomerDto : AppDto
    {
        public int CustomerId { get; set; }
        public string CustomerNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int AddressId { get; set; }
        public  AddressDto Address { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Password { get; set; }
        public  ICollection<OrderDto> Orders { get; set; }
        public ResponseDto<Customer> Response { get; set; }

        public CustomerDto()
        {
            Address = new AddressDto {Town = new TownDto()};
        }
    }
}
