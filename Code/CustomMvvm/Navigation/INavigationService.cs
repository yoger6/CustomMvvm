using System;

namespace CustomMvvm.Navigation
{
    public interface INavigationService
    {
        event EventHandler<NavigationEventArgs> NavigationRequested;
        void Navigate( Type type );
        void Navigate( Type type, object parameter );
    }
}
