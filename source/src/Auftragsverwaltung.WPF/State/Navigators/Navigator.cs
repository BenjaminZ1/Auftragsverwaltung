using System;
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
                _currentViewModel?.Dispose();

                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
                StateChanged?.Invoke();
            }
        }

        public Navigator(IAppViewModelAbstractFactory viewModelFactory)
        {
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(this, viewModelFactory);
            UpdateCurrentViewModelCommand.Execute(ViewType.Home);
        }

        public ICommand UpdateCurrentViewModelCommand { get; set; }

        public event Action StateChanged;
    }
}
