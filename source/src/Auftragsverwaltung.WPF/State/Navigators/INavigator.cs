using System.Windows.Input;
using Auftragsverwaltung.WPF.ViewModels;

namespace Auftragsverwaltung.WPF.State.Navigators
{
    public enum ViewType
    {
        Home,
        Customer,
        Article,
        Order
    }
    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        ICommand UpdateCurrentViewModelCommand { get; }
    }
}
