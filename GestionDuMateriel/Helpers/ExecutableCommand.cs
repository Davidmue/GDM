using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;

namespace GestionDuMateriel.Helpers
{
    class ExecutableCommand : ICommand
    {
        private readonly Action<object> _action;

        /// <summary>
        /// Constructor taking a delegate with parameter
        /// </summary>
        /// <param name="action">The action to perform when the command is executed</param>
        public ExecutableCommand(Action<object> action)
        {

            _action = action;
        }

        /// <summary>
        /// Constructor taking a delegate without any parameter
        /// </summary>
        /// <param name="action">The action to perform when the command is executed</param>
        public ExecutableCommand(Action action)
        {

            _action = delegate { action(); };
        }

        /// <summary>
        /// Implementation of <c>ICommand.Execute</c>
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {

            _action(parameter);
        }

        /// <summary>
        /// Implemenation of <c>ICommand.CanExecute</c>
        /// </summary>
        /// <param name="parameter">parameter, typically from XAML</param>
        /// <returns>True if the command is enabled</returns>
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Implemenation of <c>ICommand.CanExecute</c>
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            // see: http://joshsmithonwpf.wordpress.com/2008/06/17/allowing-commandmanager-to-query-your-icommand-objects/

            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}
