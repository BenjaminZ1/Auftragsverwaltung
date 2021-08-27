namespace Auftragsverwaltung.WPF.ViewModels.Factories
{
    public interface IAppViewModelFactory<T> where T : ViewModelBase
    {
        T CreateViewModel();
    }
}
