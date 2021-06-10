using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.WPF.State.Navigators;

namespace Auftragsverwaltung.WPF.ViewModels.Factories
{
    public class AppViewModelAbstractFactory : IAppViewModelAbstractFactory
    {
        private readonly CreateViewModel<HomeViewModel> _homeViewModelFactory;
        private readonly CreateViewModel<CustomerViewModel> _customerViewModelFactory;
        private readonly CreateViewModel<OrderViewModel> _orderViewModelFactory;
        private readonly CreateViewModel<ArticleViewModel> _articleViewModelFactory;

        public AppViewModelAbstractFactory(CreateViewModel<HomeViewModel> homeViewModelFactory,
            CreateViewModel<CustomerViewModel> customerViewModelFactory,
            CreateViewModel<OrderViewModel> orderViewModelFactory,
            CreateViewModel<ArticleViewModel> articleViewModelFactory)
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
                ViewType.Home => _homeViewModelFactory(),
                ViewType.Customer => _customerViewModelFactory(),
                ViewType.Article => _articleViewModelFactory(),
                ViewType.Order => _orderViewModelFactory(),
                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", nameof(viewType))
            };
        }
    }
}
