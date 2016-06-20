using CustomMvvm;
using CustomMvvm.Navigation;

namespace CustomMvvmTest
{
    internal class NavigableViewModelStub : NavigableViewModel
    {
        public INavigationService ExposedNavigation => Navigation;
        public bool NavigatedToCalled { get; private set; }
        public bool NavigatedFromCalled { get; private set; }

        public override void OnNavigatedTo()
        {
            NavigatedToCalled = true;
        }

        public override void OnNavigatedFrom()
        {
            NavigatedFromCalled = true;
        }
    }

    internal class AnotherNavigableViewModelStub : NavigableViewModel {

        public override void OnNavigatedTo()
        {
            throw new System.NotImplementedException();
        }

        public override void OnNavigatedFrom()
        {
            throw new System.NotImplementedException();
        }
    }
}