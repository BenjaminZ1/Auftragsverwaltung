using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Address;
using Auftragsverwaltung.Domain.Customer;
using AutoMapper;
using Microsoft.Win32.SafeHandles;

namespace Auftragsverwaltung.Application.Mapper.Resolver
{
    //public class AddressResolver : IValueResolver<AddressDto, Address, List<Address>>
    //{
    //    public List<Address> Resolve(AddressDto source, Address destination, List<Address> destMember, ResolutionContext context)
    //    {
    //        destMember.Add(new Address()
    //        {
    //            AddressId = source.AddressId,
    //            BuildingNr = source.BuildingNr,
    //            Customer = source.Customer
    //        });
    //        return destMember;
    //    }
    //}
}
