using Auftragsverwaltung.WPF.State.Navigators;
using Auftragsverwaltung.WPF.ViewModels.Factories;
using System;
using System.Windows.Input;

namespace Auftragsverwaltung.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly INavigator _navigator;
        private readonly IAppViewModelAbstractFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, IAppViewModelAbstractFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var type = parameter as ViewType?;
            if (type != null)
            {
                ViewType viewType = type.Value;
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}
