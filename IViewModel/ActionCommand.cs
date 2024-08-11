using System.Windows.Input;

namespace Interfaces.ViewModel
{

    /// <summary>
    /// A basic implementation of ICommand that wraps a method that takes no parameters or a method that takes one parameter.
    /// </summary>
    public sealed class ActionCommand : ICommand
    {
        private Action action;
        private Action<object> objectAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <remarks>Use this constructor to provide an action that ignores the ICommand parameter.</remarks>
        public ActionCommand(Action action)
        {
            this.action = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class.
        /// </summary>
        /// <param name="objectAction">An action that takes an object parameter.</param>
        /// <remarks>Use this constructor to provide an action that uses the object parameter passed by the Execute method.</remarks>
        public ActionCommand(Action<object> objectAction = null)
        {
            this.objectAction = objectAction;
        }

        #region ICommand Members

        public event EventHandler? CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, then this object can be set to null.</param>
        /// <returns>
        /// Always returns true.
        /// </returns>
        bool ICommand.CanExecute(object parameter = null)
        {
            return true;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, then this object can be set to null.</param>
        public void Execute(object parameter = null)
        {
            if (this.objectAction != null && parameter != null)
            {
                this.objectAction(parameter);
            }
            else
            {
                this.action();
            }
        }
        #endregion
    }
}