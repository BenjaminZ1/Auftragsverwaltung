using System.Windows.Input;
using Auftragsverwaltung.WPF.Commands;
using Auftragsverwaltung.WPF.State.Navigators;
using Auftragsverwaltung.WPF.ViewModels.Factories;

namespace Auftragsverwaltung.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; }
        private readonly IAppViewModelAbstractFactory _viewModelFactory;

        public ViewModelBase CurrentViewModel => Navigator.CurrentViewModel;


        public MainViewModel(INavigator navigator, IAppViewModelAbstractFactory viewModelFactory)
        {
            Navigator = navigator;
            _viewModelFactory = viewModelFactory;

            
        }

       
        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public override void Dispose()
        {
            Navigator.StateChanged -= Navigator_StateChanged;

            base.Dispose();
        }
    }
}
