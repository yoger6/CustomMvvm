using CustomMvvm.Validation;
using NUnit.Framework;

namespace CustomMvvmTest.ValidationTests
{
    [TestFixture] 
    public class RegexAttributeTest
    {
        private RegexAttribute _validator;

        public RegexAttributeTest()
        {
            _validator = new RegexAttribute( "^[a]*$" );
        }

        [Test]
        public void ReturnsErrorIfStringContainsNonAlphanumericChars()
        {
            var error = _validator.GetValidationError( "b" );

            Assert.IsFalse( string.IsNullOrWhiteSpace( error ) );
        }

        [Test]
        public void ReturnsNoErrorsWhenSequenceConsistsOfAlphanumericSymbols()
        {
            var error = _validator.GetValidationError( "a" );

            Assert.IsTrue( string.IsNullOrWhiteSpace( error ) );
        }
    }
}
