using Auftragsverwaltung.Application.Service;

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
            return ArticleViewModel.LoadArticleListViewModel(_articleService);
        }
    }
}
