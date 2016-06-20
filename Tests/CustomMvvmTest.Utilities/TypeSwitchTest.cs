using System;
using CustomMvvm.Utilities;
using NUnit.Framework;

namespace CustomMvvmTest.Utilities
{
    [TestFixture]
    public class TypeSwitchTest
    {
        [SetUp]
        public void Setup()
        {
            _switch = new TypeSwitch();
        }

        private TypeSwitch _switch;
        private bool _intActionInvoked;
        private bool _stringActionInvoked;

        private void StringAction()
        {
            _stringActionInvoked = true;
        }

        private void IntegerAction()
        {
            _intActionInvoked = true;
        }


        [Test]
        public void ExecuteInvokesActionAssignedToType()
        {
            _switch.Set( typeof(int), IntegerAction );
            _switch.Set( typeof(string), StringAction );

            _switch.Execute( typeof(int) );

            Assert.IsTrue( _intActionInvoked );
            Assert.IsFalse( _stringActionInvoked );
        }

        [Test]
        public void ExecutesFallbackActionAssignedInConstructorWhenExecutedTypeNotFoundOrNull()
        {
            var swithWithFallback = new TypeSwitch( StringAction );
            swithWithFallback.Set( typeof(int), IntegerAction );

            swithWithFallback.Execute( typeof(bool) );

            Assert.IsTrue( _stringActionInvoked );
        }

        [Test]
        public void ThrowsIfExecutedTypeIsNotSetAndNoFallbackSet()
        {
            TestDelegate execute = () => _switch.Execute( typeof(bool) );

            Assert.Throws<InvalidOperationException>( execute );
        }

        [Test]
        public void ThrowsIfExecutedTypeIsNullAndNoFallbackSet()
        {
            TestDelegate execute = () => _switch.Execute( null );

            Assert.Throws<InvalidOperationException>( execute );
        }

        [Test]
        public void ThrowsIfTypeIsAlreadySet()
        {
            TestDelegate setting = () => _switch.Set( typeof(int), IntegerAction );

            setting.Invoke();

            Assert.Throws<ArgumentException>( setting );
        }
    }
}