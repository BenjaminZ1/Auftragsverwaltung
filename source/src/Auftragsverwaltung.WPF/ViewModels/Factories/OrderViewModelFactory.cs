using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Application.Service;

namespace Auftragsverwaltung.WPF.ViewModels.Factories
{
    public class OrderViewModelFactory : IAppViewModelFactory<OrderViewModel>
    {
        private readonly IOrderService _orderService;

        public OrderViewModelFactory(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public OrderViewModel CreateViewModel()
        {
            return OrderViewModel.LoadOrderListViewModel(_orderService);
        }
    }
}
