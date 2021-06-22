using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Auftragsverwaltung.WPF.Commands
{
    public class BaseCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _action;
        
        public BaseCommand(Action<object> action)
            : this(action, null)
        {
        }

        public BaseCommand(Action<object> action, Predicate<object> canExecute)
        {
            this._action = action ?? throw new ArgumentNullException("action");
            this._canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public void DoCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, null);
        }
    }
}
