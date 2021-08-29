using Auftragsverwaltung.Domain.Common;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Domain.ArticleGroup
{
    public interface IOrderRepository : IAppRepository<Domain.Order.Order>
    {
        DataTable GetQuarterDataTable();
    }
}
