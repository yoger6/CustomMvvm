using System;
using System.Windows.Input;

namespace CustomMvvm.Commands
{
    /// <summary>
    ///     Command that always executes given Action
    /// </summary>
    public class ActionCommand<T> : ICommand where T : class
    {
        private readonly Action<T> _action;

        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="action">Action to be executed</param>
        public ActionCommand( Action<T> action )
        {
            if (action == null)
            {
                throw new ArgumentNullException( nameof( action ) );
            }
            _action = action;
        }

        /// <summary>
        ///     Required to raise event in derieved classes
        /// </summary>
        protected void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke( this, EventArgs.Empty );
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to null.
        /// </param>
        public virtual bool CanExecute( object parameter )
        {
            return true;
        }

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to null.
        /// </param>
        public void Execute( object parameter )
        {
            _action.Invoke( (T) parameter );
        }
    }

    /// <summary>
    ///     Command that always executes given Action
    /// </summary>
    public class ActionCommand : ICommand
    {
        private readonly Action _action;

        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="action">Action to be executed</param>
        public ActionCommand( Action action )
        {
            if (action == null)
            {
                throw new ArgumentNullException( nameof( action ) );
            }
            _action = action;
        }


        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public virtual bool CanExecute()
        {
            return true;
        }

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        public void Execute()
        {
            _action.Invoke();
        }

        /// <summary>
        ///     Required to raise event in derieved classes
        /// </summary>
        protected virtual void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke( this, EventArgs.Empty );
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        /// <param name="parameter">Parameter value is ignored by non generic ActionCommand</param>
        bool ICommand.CanExecute( object parameter )
        {
            return CanExecute();
        }

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Parameter value is ignored by non generic ActionCommand</param>
        void ICommand.Execute( object parameter )
        {
            Execute();
        }
    }
}