using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CustomMvvm.Validation;

namespace CustomMvvmTest.ValidationTests
{
    internal class ValidationAdvancedScenario : ValidationBasicScenario, IValidationWithAttributes
    {
        public int Id { get; set; }
        [AlwaysInvalid]
        public string InvalidName { get; set; }
        [AlwaysValid]
        public string ValidName { get; set; }
        private readonly List<string> _errors;

        public ValidationAdvancedScenario( Validator validator )
            : base( validator )
        {
            _errors= new List<string>();
        }


        public void CallValidationForPropertyWithoutAttribute()
        {
            CallValidation( nameof( Id ), this );
        }

        public void CallValidationForInvalidProperty()
        {
            CallValidation( nameof(InvalidName), this );
        }

        #region IValidationWithAttributes
        public bool HasErrors => _errors.Count > 0;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors( string propertyName )
        {
            return _errors;
        }

        public void RemoveErrors( string property )
        {
            _errors.Clear();
            OnErrorsChanged( null );
        }

        public void SetErrors( string property, IEnumerable<string> errors )
        {
            _errors.Clear();
            if (errors.Any(x=>x != null))
            {
                _errors.AddRange( errors );
            }
            OnErrorsChanged( null );
        }

        protected virtual void OnErrorsChanged( DataErrorsChangedEventArgs e )
        {
            ErrorsChanged?.Invoke( this, e );
        }
        #endregion

        public void CallValidationForValidProperty()
        {
            CallValidation( nameof(ValidName), this );
        }
    }

    internal class AlwaysInvalidAttribute : ValidationAttribute {
        
        public override string GetValidationError( object value )
        {
            return "This property definitely has an error.";
        }
    }

    internal class AlwaysValidAttribute : ValidationAttribute
    {
        public override string GetValidationError( object value )
        {
            return null;
        }
    }
}