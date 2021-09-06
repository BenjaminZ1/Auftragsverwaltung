using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Application.Dtos;

namespace Auftragsverwaltung.Application.Service
{
    public interface IOrderService : IAppService<OrderDto>
    {
        public DataTable GetQuarterData();
        public Task<IEnumerable<OrderOverviewDto>> GetOrderOverview();
    }
}
