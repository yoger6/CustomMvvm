using System;

namespace CustomMvvm.Validation
{
    public class RangeAttribute : ValidationAttribute
    {
        private readonly int _min;
        private readonly int _max;

        public int Min => _min;
        public int Max => _max;

        public RangeAttribute(int min, int max)
        {
            _min = min;
            _max = max;
        }
        
        public override string GetValidationError( object value )
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return TypeSpecificValidation( value, value.GetType() );
        }

        private string TypeSpecificValidation( object value, Type type )
        {
            //todo: static dictionary with type specific validators 
            //get if present else init and add 
            if (type == typeof (int))
            {
                return ValidateRange( (int)value );
            }
            if (type == typeof (string))
            {
                var length = ( (string) value ).Length;
                return ValidateRange( length );
            }

            throw new ArgumentException(nameof(type) + " is not supported by this Validator.");
        }

        private string ValidateRange( int value )
        {
            if (value < _min)
            {
                return $"The value {value} is too low - lower bound is {_min}.";
            }
            if (value > _max)
            {
                return $"The value {value} is too high - upper boun is {_max}";
            }

            return null;
        }
    }
}