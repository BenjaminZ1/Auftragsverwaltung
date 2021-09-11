namespace Auftragsverwaltung.WPF.ViewModels.Factories
{
    public interface IAppViewModelFactory<out T> where T : ViewModelBase
    {
        T CreateViewModel();
    }
}
