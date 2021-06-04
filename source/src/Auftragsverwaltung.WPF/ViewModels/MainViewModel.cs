using Auftragsverwaltung.WPF.State.Navigators;

namespace Auftragsverwaltung.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; } = new Navigator();
    }
}
