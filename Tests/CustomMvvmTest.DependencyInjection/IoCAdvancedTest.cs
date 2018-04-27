using System;
using CustomMvvm.DependencyInjection;
using CustomMvvmTest.DependencyInjection.Stubs;
using NUnit.Framework;

namespace CustomMvvmTest.DependencyInjection
{
    public partial class IoCTest
    {
        [Test]
        public void ReturnsItselfWhenRequestForIoC()
        {
            var ioc = Container.Resolve<IIoC>();

            Assert.AreSame( Container, ioc );
        }

        [Test]
        public void ReturnsNewInstanceEachTimeByDefault()
        {
            var firstInstance = Container.Resolve<ClassWithPublicCtor>();
            var secondInstance = Container.Resolve<ClassWithPublicCtor>();

            Assert.AreNotSame( firstInstance, secondInstance );
        }

        [Test]
        public void ReturnsSingleInstanceForTypesRegisteredAsSingleton()
        {
            Container.ConfigureType<Bar>().AsSingleton();
            var firstBar = Container.Resolve<Bar>();
            var secondBar = Container.Resolve<Bar>();

            Assert.AreSame( firstBar, secondBar );
            Assert.NotNull( firstBar );
        }

        [Test]
        public void CannotResolveTypeThatHasPrivateConstructor()
        {
            TestDelegate testDelegate = () => Container.Resolve<ClassWithNoPublicCtor>();

            Assert.Throws<MissingMemberException>( testDelegate );
        }

        [Test]
        public void ResolvesTypeWithParameterlessConstructorWithoutConfiguration()
        {
            var instance = Container.Resolve<ClassWithPublicCtor>();

            Assert.IsNotNull( instance );
        }

        [Test]
        public void ConstructsParametersIfConstructorRequires()
        {
            var instance = Container.Resolve<ClassWithParameterizedCtor>();
            Assert.IsNotNull( instance );
        }

        [Test]
        public void ConstructsAbstractTypesIfConfigured()
        {
            Container.ConfigureType<Bar>().For<IBar>();
            var bar = Container.Resolve<IBar>();

            Assert.NotNull( bar );
        }

        [Test]
        public void InjectsParametersWithMatchingInjectionTargetWhenConfigured()
        {
            Container.ConfigureType<Bar>().For<IBar>();
            Container.ConfigureType<AnotherBar>().For<IBar>().When<Foo>();

            var foo = Container.Resolve<Foo>();

            Assert.AreSame( typeof(AnotherBar), foo.Bar.GetType() );
        }

        [Test]
        public void CanRegisterInstanceOfTypeThatIsConfiguredAsSignleton()
        {
            var bar = new Bar();
            Container.ConfigureType<Bar>().AsSingleton();
            Container.RegisterInstance( bar );

            Assert.True( Container.ExposedInstances.ContainsValue( bar ) );
        }

        [Test]
        public void CannotRegisterInstanceIfAlreadyRegistered()
        {
            var bar = new Bar();
            Container.ConfigureType<Bar>().AsSingleton();

            TestDelegate registerAction = () => Container.RegisterInstance( bar );

            registerAction.Invoke();

            Assert.Throws<ArgumentException>( registerAction );
        }

        [Test]
        public void CannotRegisterInstanceIfNotConfiguredAsSingleton()
        {
            TestDelegate register = () => Container.RegisterInstance( new Bar() );

            Assert.Throws<InvalidOperationException>( register );
        }
    }
}