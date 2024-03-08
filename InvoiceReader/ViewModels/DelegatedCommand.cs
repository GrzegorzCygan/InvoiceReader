using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InvoiceReader.ViewModels
{
    /// <summary>
    /// Delegated command
    /// </summary>
    public class DelegatedCommand : ICommand
    {
        private readonly Func<object, bool> _canExecute;
        private readonly Action<object> _execute;

        public event EventHandler CanExecuteChanged;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="canExecute">Function that tells if command can be executed</param>
        /// <param name="execute">Command execution is delegated to this action</param>
        public DelegatedCommand(Func<object, bool> canExecute, Action<object> execute)
        {
            this._canExecute = canExecute;
            this._execute = execute;
        }
        /// <inheritdoc/>
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? false;
        }
        /// <inheritdoc/>

        public void Execute(object parameter)
        {
            _execute?.Invoke(parameter);
        }
    }
}
