using System.Windows.Input;
using Auftragsverwaltung.WPF.Commands;
using Auftragsverwaltung.WPF.State.Navigators;
using Auftragsverwaltung.WPF.ViewModels.Factories;

namespace Auftragsverwaltung.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; }
        public ICommand UpdateCurrentViewModelCommand { get; set; }

        public MainViewModel(INavigator navigator, IAppViewModelAbstractFactory viewModelFactory)
        {
            Navigator = navigator;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(Navigator, viewModelFactory);
            UpdateCurrentViewModelCommand.Execute(ViewType.Home);
        }
    }
}
