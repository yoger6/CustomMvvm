using System;
using System.ComponentModel;
using System.Linq;

namespace CustomMvvm.Commands
{
    /// <summary>
    ///     Command executing action based on solid condition.
    ///     CanExecuteChanged event is invoked when assigned properties change and
    ///     CanExecute value changes since previous call.
    ///     Listens to PropertyChanged on contexts INotifyPropertyChanged interface.
    /// </summary>
    public class WatchingCommand<T> : ManualRaiseCommand<T> where T : class
    {
        private readonly INotifyPropertyChanged _context;
        private readonly string[] _monitoredProperties;

        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="action">Action to be executed</param>
        /// <param name="condition">Condition to check if action can be executed</param>
        /// <param name="context">Object containing properties to monitor; has to implement INotifyPropertyChanged interface</param>
        /// <param name="properties">Properties which will raise CanExecuteChanged event when changes to any of them are detected</param>
        public WatchingCommand( Action<T> action, Func<bool> condition,
            object context, params string[] properties )
            : base( action, condition )
        {
            _context = context as INotifyPropertyChanged;
            if (_context == null)
            {
                throw new ArgumentException(
                    "Context doesn't implement INotifyPropertyChange hence it can't be used for monitoring dependencies." );
            }
            _context.PropertyChanged += OnPropertyChanged;
            _monitoredProperties = properties;
        }

        private void OnPropertyChanged( object sender, PropertyChangedEventArgs propertyChangedEventArgs )
        {
            if (IsPropertyMonitored( propertyChangedEventArgs.PropertyName ))
            {
                RaiseCanExecuteChanged();
            }
        }

        private bool IsPropertyMonitored( string propertyName )
        {
            return _monitoredProperties.Any( x => x.Equals( propertyName ) );
        }
    }

    /// <summary>
    ///     Command executing action based on solid condition.
    ///     CanExecuteChanged event is invoked when assigned properties change and
    ///     CanExecute value changes since previous call.
    ///     Listens to PropertyChanged on contexts INotifyPropertyChanged interface.
    /// </summary>
    public class WatchingCommand : ManualRaiseCommand
    {
        private readonly INotifyPropertyChanged _context;
        private readonly string[] _monitoredProperties;

        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="action">Action to be executed</param>
        /// <param name="condition">Condition to check if action can be executed</param>
        /// <param name="context">Object containing properties to monitor; has to implement INotifyPropertyChanged interface</param>
        /// <param name="properties">Properties which will raise CanExecuteChanged event when changes to any of them are detected</param>
        public WatchingCommand( Action action, Func<bool> condition,
            object context, params string[] properties )
            : base( action, condition )
        {
            _context = context as INotifyPropertyChanged;
            if (_context == null)
            {
                throw new ArgumentException(
                    "Context doesn't implement INotifyPropertyChange hence it can't be used for monitoring dependencies." );
            }
            _context.PropertyChanged += OnPropertyChanged;
            _monitoredProperties = properties;
        }

        private void OnPropertyChanged( object sender, PropertyChangedEventArgs propertyChangedEventArgs )
        {
            if (IsPropertyMonitored( propertyChangedEventArgs.PropertyName ))
            {
                RaiseCanExecuteChanged();
            }
        }

        private bool IsPropertyMonitored( string propertyName )
        {
            return _monitoredProperties.Any( x => x.Equals( propertyName ) );
        }
    }
}