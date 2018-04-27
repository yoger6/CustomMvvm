using CustomMvvm.Validation;
using NUnit.Framework;

namespace CustomMvvmTest.ValidationTests
{
    [TestFixture]
    public class EmailAttributeTest
    {
        private readonly EmailAttribute _attribute;

        public EmailAttributeTest()
        {
            _attribute = new EmailAttribute();
        }

        [TestCase( "emailnet.com" )]
        [TestCase( "emai@lnetcom" )]
        [TestCase( "email@@net.com" )]
        [TestCase( "email@net .com" )]
        public void InvalidEmailReturnError(string email)
        {
            var error = _attribute.GetValidationError( email );

            Assert.IsFalse( string.IsNullOrWhiteSpace( error ) );
        }

        [Test]
        public void ValidEmailHasNoErrors()
        {
            var error = _attribute.GetValidationError( "this_should@exist.com" );

            Assert.IsTrue( string.IsNullOrWhiteSpace( error ) );
        }
    }
}
