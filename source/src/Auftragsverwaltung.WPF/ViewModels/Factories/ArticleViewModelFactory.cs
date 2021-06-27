using Auftragsverwaltung.Application.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.WPF.ViewModels.Factories
{
    public class ArticleViewModelFactory : IAppViewModelFactory<ArticleViewModel>
    {
        private readonly IArticleService _articleService;

        public ArticleViewModelFactory(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public ArticleViewModel CreateViewModel()
        {
            
        }
    }
}
