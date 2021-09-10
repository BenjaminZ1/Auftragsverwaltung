using Auftragsverwaltung.WPF.ViewModels;

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
