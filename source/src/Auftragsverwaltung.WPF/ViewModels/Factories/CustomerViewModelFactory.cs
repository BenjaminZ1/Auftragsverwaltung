using Auftragsverwaltung.Application.Service;

namespace Auftragsverwaltung.WPF.ViewModels.Factories
{
    public class CustomerViewModelFactory : IAppViewModelFactory<CustomerViewModel>
    {

        private readonly ICustomerService _customerService;

        public CustomerViewModelFactory(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public CustomerViewModel CreateViewModel()
        {
            return CustomerViewModel.LoadCustomerListViewModel(_customerService);
        }
    }
}
