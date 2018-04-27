using System;
using CustomMvvm.Commands;
using NUnit.Framework;

namespace CustomMvvmTest.Commands
{
    [TestFixture]
    public class ParameterlessManualRaiseCommandTest
    {
        [Test]
        public void ThrowsExceptionIfConditionNotSet()
        {
            TestDelegate testDelegate = () => { new ManualRaiseCommand( () => { }, null ); };

            Assert.Throws<ArgumentNullException>( testDelegate );
        }

        [Test]
        public void CanExecuteIsTrueWhenConditionIsSatisfied()
        {
            var command = new ManualRaiseCommand( () => { }, () => true );

            Assert.IsTrue( command.CanExecute() );
        }

        [Test]
        public void CanExecuteIsFalseWhenConditionNotSatisfied()
        {
            var command = new ManualRaiseCommand( () => { }, () => false );

            Assert.IsFalse( command.CanExecute() );
        }

        [Test]
        public void EventNotRaisedByCallingPublicInvocatorWhenConditionDoesntChange()
        {
            var eventRaised = CallEventInvocatorAndCheckIfEventWasInvoked( false, false );

            Assert.IsFalse( eventRaised );
        }

        [Test]
        public void EventRaisedByCallingPublicInvocatorWhenConditionChanges()
        {
            var eventRaised = CallEventInvocatorAndCheckIfEventWasInvoked( false, true );

            Assert.IsTrue( eventRaised );
        }

        private static bool CallEventInvocatorAndCheckIfEventWasInvoked( bool initialCondition, bool changedCondition )
        {
            var eventRaised = false;
            var command = new ManualRaiseCommand( () => { }, () => initialCondition );
            command.CanExecuteChanged += ( sender, args ) => eventRaised = true;
            initialCondition = changedCondition;
            command.RaiseCanExecuteChanged();

            return eventRaised;
        }
    }
}