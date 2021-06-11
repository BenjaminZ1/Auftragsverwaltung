using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Application.Service;

namespace Auftragsverwaltung.WPF.ViewModels.Factories
{
    class CustomerListViewModelFactory : IAppViewModelFactory<CustomerListViewModel>
    {
        private readonly ICustomerService _customerService;

        public CustomerListViewModelFactory(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public CustomerListViewModel CreateViewModel()
        {
            return CustomerListViewModel.LoadCustomerListViewModel(_customerService);
        }
    }
}
