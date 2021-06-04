using System;
using System.Windows.Input;
using Auftragsverwaltung.WPF.State.Navigators;
using Auftragsverwaltung.WPF.ViewModels;

namespace Auftragsverwaltung.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private INavigator _navigator;

        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;
                switch(viewType)
                {
                    case ViewType.Home:
                        _navigator.CurrentViewModel = new HomeViewModel();
                        break;
                    case ViewType.Customer:
                        _navigator.CurrentViewModel = new CustomerViewModel();
                        break;
                    case ViewType.Article:
                        _navigator.CurrentViewModel = new ArticleViewModel();
                        break;
                    case ViewType.Order:
                        _navigator.CurrentViewModel = new OrderViewModel();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
