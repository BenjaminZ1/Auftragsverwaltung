using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auftragsverwaltung.WPF.Commands
{
    public class AsyncCommand : AsyncCommandBase
    {
        private readonly Func<object, Task> _command;
        private readonly Predicate<object> _canExecute;
        public AsyncCommand(Func<object, Task> command)
        {
            _command = command;
        }

        public AsyncCommand(Func<object, Task> command, Predicate<object> canExecute)
        {
            _command = command;
            _canExecute = canExecute;
        }
        public override bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }
        public override Task ExecuteAsync(object parameter)
        {
            return _command(parameter);
        }
    }
}
