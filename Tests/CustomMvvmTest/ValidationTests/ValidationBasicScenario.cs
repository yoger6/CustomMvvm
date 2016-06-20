using CustomMvvm.Validation;

namespace CustomMvvmTest.ValidationTests
{
    internal class ValidationBasicScenario
    {
        protected readonly Validator _validator;
        public int IdWithoutAttribute { get;set;}

        public ValidationBasicScenario(Validator validator)
        {
            _validator = validator;
        }

        public void CallValidationForContextWithoutInterface()
        {
            CallValidation( nameof( IdWithoutAttribute ), this );
        }

        public void CallValidation( string propertyName, object validationContext )
        {
            _validator.Validate( propertyName, validationContext );
        }
    }
}