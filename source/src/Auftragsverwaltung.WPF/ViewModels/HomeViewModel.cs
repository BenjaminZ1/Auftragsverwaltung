using System.Data;
using Auftragsverwaltung.Application.Service;

namespace Auftragsverwaltung.WPF.ViewModels
{
    public class HomeViewModel : CommonViewModel
    {
        private readonly IOrderService _orderService;

        public DataView View { get; set; }

        public HomeViewModel(IOrderService orderService)
        {
            _orderService = orderService;
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
        }
    }
}
