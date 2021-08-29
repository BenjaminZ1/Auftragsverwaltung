using Auftragsverwaltung.Application.Service;

namespace Auftragsverwaltung.WPF.ViewModels.Factories
{
    public class HomeViewModelFactory : IAppViewModelFactory<HomeViewModel>
    {
        private readonly IOrderService _orderService;

        public HomeViewModelFactory(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public HomeViewModel CreateViewModel()
        {
            return HomeViewModel.LoadOrderListViewModel(_orderService);
        }
    }
}
