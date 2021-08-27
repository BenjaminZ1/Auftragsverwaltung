using Auftragsverwaltung.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Domain.ArticleGroup
{
    public interface IArticleGroupRepository : IAppRepository<Domain.ArticleGroup.ArticleGroup>
    {
        Task<IEnumerable<Domain.ArticleGroup.ArticleGroup>> GetHierarchicalData();
    }
}
