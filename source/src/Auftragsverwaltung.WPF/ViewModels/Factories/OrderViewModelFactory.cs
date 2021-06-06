using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.WPF.ViewModels.Factories
{
    public class OrderViewModelFactory : IAppViewModelFactory<OrderViewModel>
    {
        public OrderViewModel CreateViewModel()
        {
            return new OrderViewModel();
        }
    }
}
