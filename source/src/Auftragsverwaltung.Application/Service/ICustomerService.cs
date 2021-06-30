using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Customer;

namespace Auftragsverwaltung.Application.Service
{
    public interface ICustomerService : IAppService<CustomerDto>
    {
    }
}
