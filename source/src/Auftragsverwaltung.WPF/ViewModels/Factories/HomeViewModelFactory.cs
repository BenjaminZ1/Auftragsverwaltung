using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.WPF.ViewModels.Factories
{
    public class HomeViewModelFactory : IAppViewModelFactory<HomeViewModel>
    {
        public HomeViewModel CreateViewModel()
        {
            return new HomeViewModel();
        }
    }
}
