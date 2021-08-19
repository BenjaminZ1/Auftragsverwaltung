using Auftragsverwaltung.Application.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.WPF.ViewModels.Factories
{
    public class ArticleGroupViewModelFactory : IAppViewModelFactory<ArticleGroupViewModel>
    {
        private readonly IArticleGroupService _articleGroupService;

        public ArticleGroupViewModelFactory(IArticleGroupService articleGroupService)
        {
            _articleGroupService = articleGroupService;
        }
        public ArticleGroupViewModel CreateViewModel()
        {
            return ArticleGroupViewModel.LoadArticleListViewModel(_articleGroupService);
        }
    }
}
