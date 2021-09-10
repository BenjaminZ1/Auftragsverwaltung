using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using System.Collections.Generic;
using System.Data;

namespace Auftragsverwaltung.WPF.ViewModels
{
    public class HomeViewModel : CommonViewModel
    {
        private readonly IOrderService _orderService;
        private IEnumerable<OrderOverviewDto> _orderOverview;

        public DataView View { get; set; }

        public HomeViewModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IEnumerable<OrderOverviewDto> OrderOverview
        {
            get => _orderOverview;
            set { _orderOverview = value; OnPropertyChanged(nameof(OrderOverview)); }
        }

        public static HomeViewModel LoadOrderListViewModel(IOrderService orderService)
        {
            HomeViewModel homeViewModel = new HomeViewModel(orderService);
            homeViewModel.LoadData();
            return homeViewModel;
        }

        private void LoadData()
        {
            var table = _orderService.GetQuarterData();
            View = table.DefaultView;

            _orderService.GetOrderOverview().ContinueWith(task =>
            {
                if (task.Exception == null)
                    OrderOverview = task.Result;
            });
        }
    }
}
