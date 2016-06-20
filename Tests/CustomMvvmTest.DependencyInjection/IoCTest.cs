using System.Linq;
using CustomMvvm.DependencyInjection;
using CustomMvvmTest.DependencyInjection.Stubs;
using NUnit.Framework;

namespace CustomMvvmTest.DependencyInjection
{   
    [TestFixture]
    public partial class IoCTest : TestWithIoC
    {
        [Test]
        public void HasNoPublicConstructors()
        {
            var constructors = typeof (IoC).GetConstructors();
            var isAnyPublic = constructors.Any( x => x.IsPublic );

            Assert.IsFalse( isAnyPublic );
        }

        [Test]
        public void ReturnsInitializedInstance()
        {
            var instance = IoC.Instance;

            Assert.IsNotNull( instance );
        }

        [Test]
        public void DictionaryInitialized()
        {
            Assert.IsNotNull( Container.ExposedInstances );
        }

        [Test]
        public void SetConstructorSelectorSetsItToGivenOne()
        {
            var current = Container.ExposedConstructorSelector;
            Container.SetConstructorSelector( new ConstructorSelectorStub() );
            var changed = Container.ExposedConstructorSelector;

            Assert.AreNotSame( current, changed );
        }

        [Test]
        public void SetConstructorSelectorWithNullKeepsCurrentOne()
        {
            var current = Container.ExposedConstructorSelector;
            Container.SetConstructorSelector( null );
            var changed = Container.ExposedConstructorSelector;

            Assert.AreSame( current, changed );
        }
        
        [Test]
        public void ConfigureTypeReturnsFluentTypeConfiguration()
        {
            var config = Container.ConfigureType<Bar>();

            Assert.AreSame( typeof(FluentTypeConfiguration), config.GetType() );
        }

        [Test]
        public void ConfigureTypeAddsConfigurationToCollection()
        {
            Container.ConfigureType<Bar>();

            Assert.NotNull( Container.ExposedConfigurations.FirstOrDefault( x => x.ConfiguredType == typeof(Bar) ) );
        }
    }
}
