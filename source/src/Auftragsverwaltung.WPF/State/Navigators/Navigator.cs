using System.Windows.Input;
using Auftragsverwaltung.WPF.Commands;
using Auftragsverwaltung.WPF.Models;
using Auftragsverwaltung.WPF.ViewModels;
using Auftragsverwaltung.WPF.ViewModels.Factories;

namespace Auftragsverwaltung.WPF.State.Navigators
{
    public class Navigator : ObservableObject, INavigator
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel 
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
        public ICommand UpdateCurrentViewModelCommand { get; set; }

        public Navigator(IAppViewModelAbstractFactory viewModelFactory)
        {
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(this, viewModelFactory);
        }
    }
}
