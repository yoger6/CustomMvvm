using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CustomMvvm.Validation;

namespace CustomMvvm
{
    public abstract class ValidatableViewModel : ViewModel, IValidationWithAttributes
    {
        private readonly Dictionary<string, List<string>> _errors;

        private static readonly Validator Validator = new Validator();

        public bool HasErrors => DoesPropertiesHaveErrors();


        protected ValidatableViewModel()
        {
            _errors = new Dictionary<string, List<string>>();
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;


        public void RemoveErrors( string property )
        {
            if ( _errors.ContainsKey( property ) )
            {
                _errors[property].Clear();
                OnErrorsChanged( new DataErrorsChangedEventArgs( property ) );
            }
        }

        public void SetErrors( string property, IEnumerable<string> errors )
        {
            if ( !_errors.ContainsKey( property ) )
            {
                _errors.Add( property, new List<string>( errors ) );
            }
            else
            {
                _errors[property].Clear();
                _errors[property].AddRange( errors );
            }

            OnErrorsChanged( new DataErrorsChangedEventArgs( property ) );
        }

        public IEnumerable GetErrors( string propertyName )
        {
            if ( _errors.ContainsKey( propertyName ) )
            {
                return _errors[propertyName];
            }

            return null;
        }

        protected virtual void OnErrorsChanged( DataErrorsChangedEventArgs e )
        {
            ErrorsChanged?.Invoke( this, e );
        }

        protected override void OnPropertyChanged( [CallerMemberName] string propertyName = null )
        {
            if ( propertyName == null )
            {
                return;
            }

            base.OnPropertyChanged( propertyName );

            Validator.Validate( propertyName, this );
        }


        private bool DoesPropertiesHaveErrors()
        {
            return _errors.Sum( x => x.Value.Count() ) > 0;
        }
    }
}