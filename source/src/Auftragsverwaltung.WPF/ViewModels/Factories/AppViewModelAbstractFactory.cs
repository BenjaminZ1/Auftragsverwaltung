using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.WPF.State.Navigators;

namespace Auftragsverwaltung.WPF.ViewModels.Factories
{
    public class AppViewModelAbstractFactory : IAppViewModelAbstractFactory
    {
        private readonly IAppViewModelFactory<HomeViewModel> _homeViewModelFactory;
        private readonly IAppViewModelFactory<CustomerViewModel> _customerViewModelFactory;
        private readonly IAppViewModelFactory<OrderViewModel> _orderViewModelFactory;
        private readonly IAppViewModelFactory<ArticleViewModel> _articleViewModelFactory;

        public AppViewModelAbstractFactory(IAppViewModelFactory<HomeViewModel> homeViewModelFactory,
            IAppViewModelFactory<CustomerViewModel> customerViewModelFactory,
            IAppViewModelFactory<OrderViewModel> orderViewModelFactory,
            IAppViewModelFactory<ArticleViewModel> articleViewModelFactory)
        {
            _homeViewModelFactory = homeViewModelFactory;
            _customerViewModelFactory = customerViewModelFactory;
            _orderViewModelFactory = orderViewModelFactory;
            _articleViewModelFactory = articleViewModelFactory;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.Home => _homeViewModelFactory.CreateViewModel(),
                ViewType.Customer => _customerViewModelFactory.CreateViewModel(),
                ViewType.Article => _articleViewModelFactory.CreateViewModel(),
                ViewType.Order => _orderViewModelFactory.CreateViewModel(),
                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", nameof(viewType))
            };
        }
    }
}
