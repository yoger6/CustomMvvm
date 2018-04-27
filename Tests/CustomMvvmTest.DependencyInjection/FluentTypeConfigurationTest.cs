using CustomMvvm.DependencyInjection;
using CustomMvvmTest.DependencyInjection.Stubs;
using NUnit.Framework;

namespace CustomMvvmTest.DependencyInjection
{
    [TestFixture]
    public class FluentTypeConfigurationTest
    {
        private TypeConfiguration _config;
        private FluentTypeConfiguration _fluent;

        [SetUp]
        public void Setup()
        {
            _config = new TypeConfiguration( typeof(Bar) );
            _fluent = new FluentTypeConfiguration( _config );
        }

        [Test]
        public void CallingForSetsSupportedType()
        {
            _fluent.For<IBar>();

            Assert.AreSame( typeof(IBar), _config.SupportedType );
        }

        [Test]
        public void CallingWhenSetsInjectionTargetAndReturnsSelf()
        {
            _fluent.When<Foo>();

            Assert.AreSame( typeof(Foo), _config.InjectionTarget );
        }

        [Test]
        public void CallingAsSingletonSetsSingleInstanceAndReturnsSelf()
        {
            _fluent.AsSingleton();

            Assert.True( _config.SingleInstance );
        }

        [Test]
        public void IsSupportingReturnsTrueIfRequestedTypeIsSupportedByConfiguredType()
        {
            _fluent.For<IBar>();
            var typeToSupport = typeof(IBar);

            Assert.True( _config.IsSupporting( typeToSupport ) );
        }
    }
}