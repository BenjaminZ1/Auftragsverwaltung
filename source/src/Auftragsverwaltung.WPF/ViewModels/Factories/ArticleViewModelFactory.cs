using Auftragsverwaltung.Application.Service;

namespace Auftragsverwaltung.WPF.ViewModels.Factories
{
    public class ArticleViewModelFactory : IAppViewModelFactory<ArticleViewModel>
    {
        private readonly IArticleService _articleService;
        private readonly IArticleGroupService _articleGroupService;

        public ArticleViewModelFactory(IArticleService articleService, IArticleGroupService articleGroupService)
        {
            _articleService = articleService;
            _articleGroupService = articleGroupService;
        }
        public ArticleViewModel CreateViewModel()
        {
            return ArticleViewModel.LoadArticleListViewModel(_articleService, _articleGroupService);
        }
    }
}
