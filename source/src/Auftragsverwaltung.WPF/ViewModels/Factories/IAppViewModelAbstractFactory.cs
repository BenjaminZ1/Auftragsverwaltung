using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.WPF.State.Navigators;

namespace Auftragsverwaltung.WPF.ViewModels.Factories
{
    public interface IAppViewModelAbstractFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
