using CustomMvvm.Navigation;

namespace CustomMvvm
{
    public class NavigableViewModel : ViewModel, INavigable
    {
        protected INavigationService Navigation { get; private set; }

        public virtual void OnNavigatedTo()
        {
        }

        public virtual void OnNavigatedFrom()
        {
        }
    }
}