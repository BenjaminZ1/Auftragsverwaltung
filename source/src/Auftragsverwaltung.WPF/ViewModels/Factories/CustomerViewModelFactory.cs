using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.WPF.ViewModels.Factories
{
    public class CustomerViewModelFactory : IAppViewModelFactory<CustomerViewModel>
    {
        private readonly IAppViewModelFactory<CustomerListViewModel> _customerViewModelFactory;

        public CustomerViewModelFactory(IAppViewModelFactory<CustomerListViewModel> customerViewModelFactory)
        {
            _customerViewModelFactory = customerViewModelFactory;
        }
        public CustomerViewModel CreateViewModel()
        {
            return new CustomerViewModel(_customerViewModelFactory.CreateViewModel());
        }
    }
}
