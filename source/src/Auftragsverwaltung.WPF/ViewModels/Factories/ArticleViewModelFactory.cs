using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.WPF.ViewModels.Factories
{
    public class ArticleViewModelFactory : IAppViewModelFactory<ArticleViewModel>
    {
        public ArticleViewModel CreateViewModel()
        {
            return new ArticleViewModel();
        }
    }
}
