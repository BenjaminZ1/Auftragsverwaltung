using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Article;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Application.Service
{
    public interface IArticleGroupService : IAppService<ArticleGroupDto>
    {
        public Task<IEnumerable<ArticleGroupDto>> GetHierarchicalData();
    }
}
