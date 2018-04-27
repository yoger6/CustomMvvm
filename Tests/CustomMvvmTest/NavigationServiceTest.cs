using System;
using CustomMvvm.Navigation;
using NUnit.Framework;

namespace CustomMvvmTest
{
    [TestFixture]
    public class NavigationServiceTest
    {
        private NavigationService _service;

        [SetUp]
        public void Setup()
        {
            _service = new NavigationService();
        }

        [Test]
        public void RaisesNavigationRequestedUponValidNavigationCall()
        {
            var raised = false;
            _service.NavigationRequested += ( sender, args ) => raised = true;

            _service.Navigate( typeof(NavigableViewModelStub) );

            Assert.True( raised );
        }

        [Test]
        public void EventArgsContainTargetTypeAndParameter()
        {
            var targetType = typeof(NavigableViewModelStub);
            var parameter = new List();

            var args = GetArgs( targetType, parameter );

            Assert.AreSame( targetType, args.TargetType );
            Assert.AreSame( parameter, args.Parameter );
        }

        [Test]
        public void CurrentLocationSetAfterNavigation()
        {
            var args = GetArgs( typeof(NavigableViewModelStub), null );

            Assert.AreSame( args.TargetType, _service.CurrentLocation );
        }

        [Test]
        public void CannotGoBackIfIsAtInitialViewModel()
        {
            Assert.False( _service.CanGoBack );
        }

        [Test]
        public void CanGoBackWhenThereAreViewModelsPreviouslyVisited()
        {
            _service.Navigate( typeof( NavigableViewModelStub ) );
            _service.Navigate( typeof( AnotherNavigableViewModelStub ) );

            Assert.True( _service.CanGoBack );
        }

        [Test]
        public void GoBackWhenCannotThrowsException()
        {
            TestDelegate action = () => _service.GoBack();

            Assert.Throws<InvalidOperationException>( action );
        }

        [Test]
        public void GoingBackRequestsNavigationToPreviouslyVisitedType()
        {
            _service.Navigate( typeof( NavigableViewModelStub ) );
            _service.Navigate( typeof( AnotherNavigableViewModelStub ) );

            _service.GoBack();

            Assert.AreSame( typeof(NavigableViewModelStub), _service.CurrentLocation );
        }

        [Test]
        public void NavigatingBackDoesntPreserveCurrentTypeInNavigationHistory()
        {
            _service.Navigate( typeof( NavigableViewModelStub ) );
            _service.Navigate( typeof( AnotherNavigableViewModelStub ) );

            _service.GoBack();

            Assert.False( _service.CanGoBack );
        }
        
        private NavigationEventArgs GetArgs( Type navigateTo, object parameter )
        {
            NavigationEventArgs arguments = null;
            _service.NavigationRequested += ( sender, args ) => arguments = args;

            _service.Navigate( navigateTo, parameter );

            return arguments;
        }
    }
}