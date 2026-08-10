using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LangApp.Admin.WPF.Infrastructure
{
    class RelayCommand : ICommand
    {
        private readonly Predicate<object>? canExecute;
        private readonly Action<object>? execute;
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public RelayCommand(Action<object>? execute, Predicate<object>? canExecute = null)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            execute?.Invoke(parameter);
        }
    }
}
