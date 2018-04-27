using CustomMvvm.Validation;
using NUnit.Framework;

namespace CustomMvvmTest.ValidationTests
{
    [TestFixture]
    public class AlphanumericTest
    {
        private readonly AlphanumericAttribute _validator;

        public AlphanumericTest()
        {
            _validator = new AlphanumericAttribute();
        }

        [Test]
        public void NonAlphanumericCharactersCauseError()
        {
            var error = _validator.GetValidationError( "," );

            Assert.IsFalse( string.IsNullOrWhiteSpace( error ) );
        }

        [Test]
        public void CorrectStringWithNoErrors()
        {
            var error = _validator.GetValidationError( "thatsGoodEnough123" );

            Assert.IsTrue( string.IsNullOrWhiteSpace( error ) );
        }
    }
}