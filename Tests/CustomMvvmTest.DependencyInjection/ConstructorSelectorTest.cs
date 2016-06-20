using System;
using System.Linq;
using CustomMvvm.DependencyInjection;
using CustomMvvmTest.DependencyInjection.Stubs;
using NUnit.Framework;

namespace CustomMvvmTest.DependencyInjection
{
    [TestFixture]
    public class ConstructorSelectorTest
    {
        private readonly ConstructorSelector _selector = new ConstructorSelector();

        [Test]
        public void ThrowsExceptionIfNoPublicConstructorIsFound()
        {
            TestDelegate testDelegate = () => _selector.GetConstructor<ClassWithNoPublicCtor>();

            Assert.Throws<MissingMemberException>( testDelegate );
        }

        [Test]
        public void ReturnsParameterlessConstructorIfAvailable()
        {
            var ctor = _selector.GetConstructor<ClassWithPublicCtor>();

            Assert.IsTrue( ctor.IsPublic && !ctor.GetParameters().Any());
        }

        [Test]
        public void ReturnsConstructorDecoratedWithPrefferredConstructorAttribute()
        {
            var ctor = _selector.GetConstructor<ClassWithTwoPublicCtors>();
            
            Assert.IsTrue( ctor.GetParameters().Length == 1 );
        }

        [Test]
        public void ReturnsFirstConstructorWithReferenceTypeParameters()
        {
            var ctor = _selector.GetConstructor<ClassWithParameterizedCtor>();

            Assert.AreEqual( 2, ctor.GetParameters().Length );
        }
    }
}
