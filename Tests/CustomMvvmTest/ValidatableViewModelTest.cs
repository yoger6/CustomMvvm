using System.Linq;
using CustomMvvm;
using CustomMvvm.Validation;
using NUnit.Framework;

namespace CustomMvvmTest
{
    [TestFixture]
    public class ValidatableViewModelTest
    {
        private ValidationTestViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _viewModel = new ValidationTestViewModel();
        }
        
        
        [Test]
        public void ImplementsIValidationWithAttributes()
        {
            Assert.IsTrue( typeof (IValidationWithAttributes).IsAssignableFrom( typeof (ValidatableViewModel) ) );
        }

        [Test]
        public void ShouldRaiseEventAndContainError()
        {
            var eventRised = false;
            _viewModel.ErrorsChanged += ( sender, args ) => eventRised = true;

            _viewModel.Id = 3;

            Assert.IsTrue( eventRised );
            Assert.IsTrue( _viewModel.HasErrors );
            CollectionAssert.IsNotEmpty( _viewModel.GetErrors( nameof( _viewModel.Id ) ) );
        }

        [Test]
        public void ShouldRaiseEventAndContainNoErrors()
        {
            _viewModel.Id = 3;
            var eventRised = false;
            _viewModel.ErrorsChanged += ( sender, args ) => eventRised = true;

            _viewModel.Id = 2;

            Assert.IsTrue( eventRised );
            Assert.IsFalse( _viewModel.HasErrors ); 
            CollectionAssert.IsEmpty( _viewModel.GetErrors( nameof( _viewModel.Id ) ) );
        }

        [Test]
        public void PropertyValidationReplacesOldErrorsWithNewOnes()
        {
            _viewModel.Id = 3;
            var errors = _viewModel.GetErrors( nameof( _viewModel.Id ) ).Cast<string>().ToArray();
            _viewModel.Id = -1;
            var updatedErrors = _viewModel.GetErrors( nameof( _viewModel.Id ) ).Cast<string>().ToArray();

            CollectionAssert.AreNotEquivalent( errors, updatedErrors );
        }
    }
}
