using System;
using System.Linq;
using System.Reflection;

namespace CustomMvvm.Validation
{
    public class Validator
    {
        public void Validate( string property, object validationContext )
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException( nameof( validationContext ) );
            }
            if (string.IsNullOrWhiteSpace(property))
            {
                throw new ArgumentException( nameof( property ) + " cannot be null, empty or whitespace." );
            }
            if (IsImplementingInterface( validationContext ))
            {
                throw new NotImplementedException(nameof( validationContext ) + " doesn't implement interface reqired for property validation.");
            }
            var attributes = GetValidationAttributes( property, validationContext );

            if (!attributes.Any())
            {
                return;
            }

            var contextInterface = (IValidationWithAttributes)validationContext;
            var propertyValue = GetValue( property, validationContext );
            Validate( contextInterface, property, propertyValue, attributes );
        }


        private void Validate( IValidationWithAttributes contextInterface, string propertyName, object propertyValue, ValidationAttribute[] attributes )
        {
            var errors = attributes.Select( x => x.GetValidationError( propertyValue ))
                                   .Where( x=>x != null );
            if (!errors.Any())
            {
                contextInterface.RemoveErrors( propertyName );
            }
            else
            {
                contextInterface.SetErrors( propertyName, errors );
            }
        }

        private ValidationAttribute[] GetValidationAttributes( string property, object validationContext )
        {
            return validationContext.GetType()
                                    .GetRuntimeProperty( property )
                                    .GetCustomAttributes()
                                    .Where( x=> typeof(ValidationAttribute)
                                        .GetTypeInfo()
                                        .IsAssignableFrom( x.GetType().GetTypeInfo() ) )
                                    .Cast<ValidationAttribute>()
                                    .ToArray();
        }

        private object GetValue( string property, object validationContext )
        {
            return validationContext.GetType()
                .GetRuntimeProperty( property )
                .GetValue( validationContext );
        }

        private bool IsImplementingInterface( object validationContext )
        {
            return !validationContext.GetType()
                                     .GetTypeInfo()
                                     .ImplementedInterfaces
                                     .Contains( typeof ( IValidationWithAttributes ) );
        }
    }
}
