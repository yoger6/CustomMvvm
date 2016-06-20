using System;

namespace CustomMvvm.Validation
{
    [AttributeUsage( AttributeTargets.Property, AllowMultiple = true)]
    public abstract class ValidationAttribute : Attribute
    {
        public abstract string GetValidationError( object value );
    }
}