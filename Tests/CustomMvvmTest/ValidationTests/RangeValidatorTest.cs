using System;
using NUnit.Framework;
using RangeAttribute = CustomMvvm.Validation.RangeAttribute;

namespace CustomMvvmTest.ValidationTests
{
    [TestFixture]
    public class RangeValidatorTest
    {
        private readonly RangeAttribute _intAndStringCaseAttribute;

        public RangeValidatorTest()
        {
            _intAndStringCaseAttribute = new RangeAttribute(2,10);
        }

        [Test]
        public void ValueCannotBeNull()
        {
            TestDelegate testDelegate = () => _intAndStringCaseAttribute.GetValidationError( null );

            Assert.Throws<ArgumentNullException>( testDelegate );
        }

        [Test]
        public void ArgumentIsNotSupportedType()
        {
            TestDelegate testDelegate = () => _intAndStringCaseAttribute.GetValidationError( new object() );

            Assert.Throws<ArgumentException>( testDelegate );
        }

        [TestCase( 1 )]
        [TestCase( "s" )]
        public void ErrorIfValueExceedsLowerBound(object value)
        {
            Assert.IsTrue( !string.IsNullOrWhiteSpace( GetErrorFor( value ) ) );
        }

        [TestCase( 11 )]
        [TestCase( "qwertyuiopa" )]
        public void ErrorIfValueExceedsUpperBound( object value )
        {
            Assert.IsTrue( !string.IsNullOrWhiteSpace( GetErrorFor( value ) ) );
        }

        [TestCase( 5 )]
        [TestCase( "qwe" )]
        public void NoErrorIfValueWithinBounds(object value)
        {
            Assert.IsTrue( string.IsNullOrWhiteSpace( GetErrorFor( value ) ) );
        }
        
        private string GetErrorFor( object value )
        {
            return _intAndStringCaseAttribute.GetValidationError( value );
        }
    }
}
