using System;

namespace CustomMvvm.Commands
{
    /// <summary>
    ///     Command that executes action based on fixed condition (parameterless).
    ///     CanExecuteChanged can be raised only by calling
    ///     RaiseCanExecuteChanged method.
    ///     Event won't be raised if new result for condition equals previous one.
    /// </summary>
    public class ManualRaiseCommand<T> : ActionCommand<T> where T : class
    {
        private readonly Func<bool> _condition;
        private bool _lastCanExecuteState;

        /// <summary>
        ///     Default constuctor
        /// </summary>
        /// <param name="action">Action to be executed</param>
        /// <param name="condition">Condition to check if action can be executed</param>
        public ManualRaiseCommand( Action<T> action, Func<bool> condition )
            : base( action )
        {
            if (condition == null)
            {
                throw new ArgumentNullException( nameof( condition ) +
                                                 " cannot be null, for command without CanExecute condition use ActionCommand" );
            }

            _condition = condition;
            _lastCanExecuteState = _condition.Invoke();
        }

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to null.
        /// </param>
        public override bool CanExecute( object parameter )
        {
            _lastCanExecuteState = _condition.Invoke();

            return _lastCanExecuteState;
        }

        /// <summary>
        ///     Manually raises CanExecuteChangedEvent so the listeners know if it is possible to execute command in current state.
        /// </summary>
        public new void RaiseCanExecuteChanged()
        {
            if (_lastCanExecuteState != CanExecute( null ))
            {
                base.RaiseCanExecuteChanged();
            }
        }
    }

    /// <summary>
    ///     Command that executes action based on fixed condition (parameterless).
    ///     CanExecuteChanged can be raised only by calling
    ///     RaiseCanExecuteChanged method.
    ///     Event won't be raised if new result for condition equals previous one.
    /// </summary>
    public class ManualRaiseCommand : ActionCommand
    {
        private readonly Func<bool> _condition;
        private bool _lastCanExecuteState;

        /// <summary>
        ///     Default constuctor
        /// </summary>
        /// <param name="action">Action to be executed</param>
        /// <param name="condition">Condition to check if action can be executed</param>
        public ManualRaiseCommand( Action action, Func<bool> condition )
            : base( action )
        {
            if (condition == null)
            {
                throw new ArgumentNullException( nameof( condition ) +
                                                 " cannot be null, for command without CanExecute condition use ActionCommand" );
            }

            _condition = condition;
            _lastCanExecuteState = _condition.Invoke();
        }

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public override bool CanExecute()
        {
            _lastCanExecuteState = _condition.Invoke();

            return _lastCanExecuteState;
        }

        /// <summary>
        ///     Manually raises CanExecuteChangedEvent so the listeners know if it is possible to execute command in current state.
        /// </summary>
        public new void RaiseCanExecuteChanged()
        {
            if (_lastCanExecuteState != CanExecute())
            {
                base.RaiseCanExecuteChanged();
            }
        }
    }
}