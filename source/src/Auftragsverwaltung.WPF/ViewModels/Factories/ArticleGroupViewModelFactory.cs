using Auftragsverwaltung.Application.Service;

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
