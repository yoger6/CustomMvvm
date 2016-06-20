using System;
using CustomMvvm.Validation;
using NUnit.Framework;

namespace CustomMvvmTest.ValidationTests
{
    [TestFixture]
    public class ValidatorTest
    {
        private Validator _validator;
        private ValidationAdvancedScenario _advancedScenario;

        public ValidatorTest()
        {
            _validator = new Validator();
        }

        [SetUp]
        public void Setup()
        {
            _advancedScenario = new ValidationAdvancedScenario( _validator );
        }

        [Test]
        public void CallWithNoValidationContextThrowsException()
        {
            TestDelegate testDelegate = () => _validator.Validate( "Property", null );

            Assert.Throws<ArgumentNullException>( testDelegate );
        }
        
        [Test]
        public void CallWithNoPropertyNameThrowsException()
        {
            TestDelegate testDelegate = () => _validator.Validate( string.Empty, this );

            Assert.Throws<ArgumentException>( testDelegate );
        }

        [Test]
        public void ValidationContextDoesntImplementInterface()
        {
            var baseTestFixture = new ValidationBasicScenario( _validator );

            TestDelegate testDelegate = () => baseTestFixture.CallValidationForContextWithoutInterface();

            Assert.Throws<NotImplementedException>( testDelegate );
        }

        [Test]
        public void ValidatedPropertyDoesntHaveAnyValidationAttributesReturnsNoErrors()
        {
            _advancedScenario.CallValidationForPropertyWithoutAttribute();
            
            Assert.IsFalse( _advancedScenario.HasErrors );
        }

        [Test]
        public void InvalidPropertyValidationReturnsErrors()
        {
            _advancedScenario.CallValidationForInvalidProperty();

            Assert.IsTrue( _advancedScenario.HasErrors );
        }

        [Test]
        public void ValidPropertyValidationReturnsNoErrors()
        {
            _advancedScenario.CallValidationForValidProperty();
            Assert.IsFalse( _advancedScenario.HasErrors );
        }
    }
}
