using System;
using CustomMvvm.Commands;
using NUnit.Framework;

namespace CustomMvvmTest.Commands
{
    [TestFixture]
    public class ActionCommandTest
    {
        [Test]
        public void ConstructorThrowsExceptionIfActionParameterIsNull()
        {
            TestDelegate testDelegate = () => new ActionCommand<object>( null );

            Assert.Throws<ArgumentNullException>( testDelegate );
        }

        [Test]
        public void ExecuteInvokesAction()
        {
            var wasInvoked = WasActionInvoked( null );

            Assert.IsTrue( wasInvoked );
        }

        [Test]
        public void ExecuteInvokesActionWithParameter()
        {
            var wasInvoked = WasActionInvoked( 1 );

            Assert.IsTrue( wasInvoked );
        }

        private bool WasActionInvoked( object parameter )
        {
            var wasInvoked = false;
            var command = new ActionCommand<object>( obj => wasInvoked = true );

            command.Execute( parameter );

            return wasInvoked;
        }

        [Test]
        public void CommandCanBeExecutedByDefault()
        {
            var command = new ActionCommand<object>( o => { } );

            Assert.IsTrue( command.CanExecute( null ) );
        }
    }
}