using Auftragsverwaltung.Domain.Common;
using System.Data;

namespace Auftragsverwaltung.Domain.ArticleGroup
{
    public interface IOrderRepository : IAppRepository<Domain.Order.Order>
    {
        DataTable GetQuarterDataTable();
    }
}
