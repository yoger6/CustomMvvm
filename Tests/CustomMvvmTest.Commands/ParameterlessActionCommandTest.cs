using System;
using CustomMvvm.Commands;
using NUnit.Framework;

namespace CustomMvvmTest.Commands
{
    [TestFixture]
    public class ParameterlessActionCommandTest
    {
        [Test]
        public void ConstructorThrowsExceptionIfActionParameterIsNull()
        {
            TestDelegate testDelegate = () => new ActionCommand( null );

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

        [Test]
        public void CommandCanBeExecutedByDefault()
        {
            var command = new ActionCommand<object>( o => { } );

            Assert.IsTrue( command.CanExecute( null ) );
        }

        private static bool WasActionInvoked( object parameter )
        {
            var wasInvoked = false;
            var command = new ActionCommand<object>( obj => wasInvoked = true );

            command.Execute( parameter );

            return wasInvoked;
        }
    }
}