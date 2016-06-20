using System;
using System.Collections.Generic;

namespace CustomMvvm.Utilities
{
    /// <summary>
    ///     Allows switching on Type, doesn't allow returning value.
    /// </summary>
    public class TypeSwitch
    {
        private readonly Dictionary<Type, Action> _cases;
        private readonly Action _fallback;

        /// <summary>
        ///     Default constructor without fallback, meaning that it will throw exceptions in unexpected situations.
        /// </summary>
        public TypeSwitch()
        {
            _cases = new Dictionary<Type, Action>();
        }

        /// <summary>
        ///     Constructor with fallback
        /// </summary>
        /// <param name="fallback">Method that will be invoked instead of throwing exceptions. Equivalent of default case.</param>
        public TypeSwitch( Action fallback )
            : this()
        {
            _fallback = fallback;
        }

        /// <summary>
        ///     Registers method to be invoked when type is mached.
        /// </summary>
        /// <param name="type">Type to handle</param>
        /// <param name="action">Method that will be invoked for that type</param>
        public void Set( Type type, Action action )
        {
            _cases.Add( type, action );
        }

        /// <summary>
        ///     Invokes method set for matching type.
        /// </summary>
        /// <param name="type">Type to match against cases</param>
        public void Execute( Type type )
        {
            if (type == null)
            {
                ExecuteFallback();
                return;
            }

            if (_cases.ContainsKey( type ))
            {
                _cases[type].Invoke();
            }
            else
            {
                ExecuteFallback( type );
            }
        }

        private void ExecuteFallback( Type type = null )
        {
            if (_fallback == null)
            {
                throw new InvalidOperationException(
                    $"Switching failed, there's no case handling {type}, consider using constructor with fallback action to handle unexpected cases." );
            }
            _fallback.Invoke();
        }
    }

    /// <summary>
    ///     Allows switching on Type, allows to return value.
    /// </summary>
    public class TypeSwitch<TResult>
    {
        private readonly Dictionary<Type, Func<TResult>> _cases;
        private readonly Func<TResult> _fallback;

        /// <summary>
        ///     Default constructor without fallback, meaning that it will throw exceptions in unexpected situations.
        /// </summary>
        public TypeSwitch()
        {
            _cases = new Dictionary<Type, Func<TResult>>();
        }

        /// <summary>
        ///     Constructor with fallback
        /// </summary>
        /// <param name="fallback">Method that will be invoked instead of throwing exceptions. Equivalent of default case.</param>
        public TypeSwitch( Func<TResult> fallback )
            : this()
        {
            _fallback = fallback;
        }

        /// <summary>
        ///     Registers method to be invoked when type is mached.
        /// </summary>
        /// <param name="type">Type to handle</param>
        /// <param name="action">Method that will be invoked for that type</param>
        public void Set( Type type, Func<TResult> action )
        {
            if (action == null)
            {
                throw new ArgumentNullException( nameof( action ) );
            }

            _cases.Add( type, action );
        }

        /// <summary>
        ///     Invokes method set for matching type.
        /// </summary>
        /// <param name="type">Type to match against cases</param>
        /// <returns>Value of TResult</returns>
        public TResult Execute( Type type )
        {
            if (type == null)
            {
                return ExecuteFallback();
            }

            if (_cases.ContainsKey( type ))
            {
                return _cases[type].Invoke();
            }
            return ExecuteFallback( type );
        }

        private TResult ExecuteFallback( Type type = null )
        {
            if (_fallback == null)
            {
                throw new InvalidOperationException(
                    $"Switching failed, there's no case handling {type}, consider using constructor with fallback action to handle unexpected cases." );
            }
            return _fallback.Invoke();
        }
    }
}