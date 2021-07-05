using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.Application.Service
{
    public interface IOrderService : IAppService<OrderDto>
    {
    }
}
