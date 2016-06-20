using System;
using CustomMvvm.Commands;
using NUnit.Framework;

namespace CustomMvvmTest.Commands
{
    [TestFixture]
    public class ParameterlessWatchingCommandTest
    {
        private CommandTestViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _viewModel = new CommandTestViewModel( 10 );
        }

        [Test]
        public void ThrowsIfContextDoesntImplementINotifyPropertyChanged()
        {
            TestDelegate testDelegate = InstantiateWatchingCommandWithInvalidContext;

            Assert.Throws<ArgumentException>( testDelegate );
        }

        private void InstantiateWatchingCommandWithInvalidContext()
        {
            new WatchingCommand( () => { }, () => false, this );
        }

        [Test]
        public void EventRisedByPropertyChangeToDesiredValue()
        {
            var wasRised = ChangeIdAndCheckIfEventWasRised( 10 );

            Assert.IsTrue( wasRised );
        }

        [Test]
        public void EventNotRisedByPropertyChangeConditionNotSatisfied()
        {
            var wasRised = ChangeIdAndCheckIfEventWasRised( 9 );

            Assert.IsFalse( wasRised );
        }

        private bool ChangeIdAndCheckIfEventWasRised( int id )
        {
            var wasRised = false;
            _viewModel.ParameterlessCommand.CanExecuteChanged += ( sender, args ) => wasRised = true;

            _viewModel.Id = id;

            return wasRised;
        }
    }
}