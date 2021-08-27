using Auftragsverwaltung.Application.Service;

namespace Auftragsverwaltung.WPF.ViewModels.Factories
{
    public class OrderViewModelFactory : IAppViewModelFactory<OrderViewModel>
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IArticleService _articleService;

        public OrderViewModelFactory(IOrderService orderService, ICustomerService customerService, IArticleService articleService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _articleService = articleService;
        }

        public OrderViewModel CreateViewModel()
        {
            return OrderViewModel.LoadOrderListViewModel(_orderService, _customerService, _articleService);
        }
    }
}
