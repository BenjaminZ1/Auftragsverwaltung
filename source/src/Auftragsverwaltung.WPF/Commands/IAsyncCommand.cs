using System.Threading.Tasks;
using System.Windows.Input;

namespace Auftragsverwaltung.WPF.Commands
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}
