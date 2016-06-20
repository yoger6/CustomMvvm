using System;
using CustomMvvm;
using NUnit.Framework;

namespace CustomMvvmTest
{
    [TestFixture]
    public class EntityViewModelTest
    {
        private TestEntityViewModel _viewModel;
        private TestEntity _model;

        [SetUp]
        public void Setup()
        {
            _model = new TestEntity {Id = 1};
            _viewModel = new TestEntityViewModel( _model );
        }

        [Test]
        public void ImplementsValidation()
        {
            Assert.True( typeof(ValidatableViewModel).IsAssignableFrom( typeof(EntityViewModel<TestEntity>) ) );
        }

        [Test]
        public void NullModelThrowsException()
        {
            TestDelegate createVm = () => new TestEntityViewModel( null );

            Assert.Throws<ArgumentNullException>( createVm );
        }

        [Test]
        public void ModelIsValidWhenHasNoErrors()
        {
            Assert.False( _viewModel.HasErrors );
            Assert.True( _viewModel.IsValid );
        }

        [Test]
        public void ModelIsNotValidIfHasErrors()
        {
            _viewModel.SetErrors( "Property", new[] {"Serous error"} );

            Assert.False( _viewModel.IsValid );
        }

        [Test]
        public void SaveCommandCanNotExecuteWhenViewModelIsInvalid()
        {
            _viewModel.SetErrors( "Property", new[] {"Serious error."} );
            var canExecute = _viewModel.SaveCommand.CanExecute( null );

            Assert.False( canExecute );
        }

        [Test]
        public void SaveCommandCanExecuteWhenViewModelIsValid()
        {
            var canExecute = _viewModel.SaveCommand.CanExecute( null );

            Assert.True( canExecute );
        }

        [Test]
        public void ExecutingSaveCommandSavesChangesToModelEntity()
        {
            _viewModel.Id = 2;
            _viewModel.SaveCommand.Execute( null );

            Assert.AreEqual( 2, _model.Id );
        }

        [Test]
        public void HasChangesWhenPropertiesDontMatchWrappedModel()
        {
            _viewModel.Id = 3;

            Assert.True( _viewModel.HasChanges );
        }

        [Test]
        public void PropertyChangedRaisedWhenModelIsModified()
        {
            var propertyChangedRaisedTimes = 0;
            _viewModel.PropertyChanged += ( sender, args ) =>
            {
                if (args.PropertyName == nameof( _viewModel.HasChanges ))
                {
                    propertyChangedRaisedTimes++;
                }
            };

            _viewModel.Id = 5;

            Assert.AreEqual( 1, propertyChangedRaisedTimes );
        }

        [Test]
        public void PropertyChangedForHasChangedRaisedOnlyOnce()
        {
            var propertyChangedRaisedTimes = 0;
            _viewModel.PropertyChanged += ( sender, args ) =>
            {
                if (args.PropertyName == nameof( _viewModel.HasChanges ))
                {
                    propertyChangedRaisedTimes++;
                }
            };

            _viewModel.Id = 5;
            _viewModel.Id = 6;

            Assert.AreEqual( 1, propertyChangedRaisedTimes );
        }

        [Test]
        public void HasBeenAddedIsFalseByDefault()
        {
            Assert.False( _viewModel.HasBeenAdded );
        }
    }
}
