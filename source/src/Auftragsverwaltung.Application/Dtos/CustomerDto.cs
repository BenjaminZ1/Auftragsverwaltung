using System;
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
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int AddressId { get; set; }
        public  AddressDto Address { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public Byte[] Password { get; set; }
        public  ICollection<OrderDto> Orders { get; set; }
        public ResponseDto<Customer> Response { get; set; }

        public CustomerDto()
        {
            Address = new AddressDto {Town = new TownDto()};
        }

        public CustomerDto(Customer customer)
        {
            CustomerId = customer.CustomerId;
            Firstname = customer.Firstname;
            Lastname = customer.Lastname;
            AddressId = customer.AddressId;
            //Address = customer.Address;
            Email = customer.Email;
            Website = customer.Website;
            Password = customer.Password;
            //Orders = customer.Orders;
        }

        public CustomerDto(ResponseDto<Customer> response)
        {
            Response = response;
            if (response.Entity != null)
            {
                CustomerId = response.Entity.CustomerId;
                Firstname = response.Entity.Firstname;
                Lastname = response.Entity.Lastname;
                AddressId = response.Entity.AddressId;
                //Address = response.Entity.Address;
                Email = response.Entity.Email;
                Website = response.Entity.Website;
                Password = response.Entity.Password;
                //Orders = response.Entity.Orders;
            }
            response.Entity = null;
        }
    }
}
