using System;
using CustomMvvm;
using CustomMvvm.Navigation;
using Moq;
using NUnit.Framework;

namespace CustomMvvmTest
{
    internal class MainViewModelTestImplementation : MainViewModelBase
    {
        public ViewModel FakeNavigatedViewModel { get; }

        public MainViewModelTestImplementation( INavigationService navigation ) : base( navigation )
        {
            FakeNavigatedViewModel = new NavigableViewModelStub();
        }

        protected override ViewModel GetInstanceToNavigateTo( Type type )
        {
            return FakeNavigatedViewModel;
        }
    }

    [TestFixture]
    public class MainViewModelBaseTest
    {
        private Mock<INavigationService> _navigationServiceMock;
        private MainViewModelTestImplementation _viewModel;

        public MainViewModelBaseTest()
        {
            _navigationServiceMock = new Mock<INavigationService>();
            _viewModel = new MainViewModelTestImplementation( _navigationServiceMock.Object );
        }

        [Test]
        public void NavigationServiceCannotBeNull()
        {
            TestDelegate action = () => new MainViewModelTestImplementation( null );

            Assert.Throws<ArgumentNullException>( action );
        }

        [Test]
        public void WhenNavigationOccursTargetTypeIsBeingCreatedAndSetAsCurrentViewModel()
        {
            RaiseNavigationRequestEvent();

            Assert.AreSame( _viewModel.FakeNavigatedViewModel, _viewModel.CurrentViewModel );
        }

        private void RaiseNavigationRequestEvent()
        {
            _navigationServiceMock.Raise(x=>x.NavigationRequested += null, new NavigationEventArgs( typeof(ViewModel) ));
        }
    }
}