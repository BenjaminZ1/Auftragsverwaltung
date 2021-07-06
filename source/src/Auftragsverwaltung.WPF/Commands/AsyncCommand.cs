using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auftragsverwaltung.WPF.Commands
{
    public class AsyncCommand : AsyncCommandBase
    {
        private readonly Func<object, Task> _command;
        public AsyncCommand(Func<object, Task> command)
        {
            _command = command;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override Task ExecuteAsync(object parameter)
        {
            return _command(parameter);
        }
    }
}
