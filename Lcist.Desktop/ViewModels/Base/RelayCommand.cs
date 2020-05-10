using System;
using System.Windows.Input;

namespace Lcist.Desktop.ViewModels.Base
{
    public class RelayCommand : ICommand
    {
        #region Properties

        private readonly Action<object> _execute;

        private readonly Predicate<object> _canExecute;

        #endregion

        #region Constructors 

        private RelayCommand() { }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute) : this(execute, null) { }

        #endregion

        #region ICommand implementation

        public bool CanExecute(object parameter) => _canExecute == null ? true : _canExecute(parameter);

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        #endregion
    }
}
