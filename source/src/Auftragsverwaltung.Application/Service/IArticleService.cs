using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Article;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.Application.Service
{
    public interface IArticleService : IAppService<ArticleDto, Article>
    {
    }
}
