using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.WPF.ViewModels.Factories
{
    public class CustomerViewModelFactory : IAppViewModelFactory<CustomerViewModel>
    {
        public CustomerViewModel CreateViewModel()
        {
            return new CustomerViewModel();
        }
    }
}
