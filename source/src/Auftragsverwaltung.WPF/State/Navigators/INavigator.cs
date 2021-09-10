using Auftragsverwaltung.WPF.ViewModels;
using System.Windows.Input;

namespace Auftragsverwaltung.WPF.State.Navigators
{
    public enum ViewType
    {
        Home,
        Customer,
        Article,
        ArticleGroup,
        Order
    }
    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
    }
}
