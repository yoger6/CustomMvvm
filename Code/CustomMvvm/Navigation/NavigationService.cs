using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomMvvm.Navigation
{
    public sealed class NavigationService : INavigationService
    {
        public event EventHandler<NavigationEventArgs> NavigationRequested;

        public bool CanGoBack => _navigationHistory.Any();
        public Type CurrentLocation { get; private set; }

        private Stack<Type> _navigationHistory;

        public NavigationService()
        {
            _navigationHistory = new Stack<Type>();
        }

        public void GoBack()
        {
            if (!CanGoBack)
            {
                throw new InvalidOperationException(
                    "Cannot go back when there's no past elements to go back to. " +
                    "Make sure to check CanGoBack before calling GoBack method" );
            }
            var args = new NavigationEventArgs( _navigationHistory.Pop() );

            Navigate( args );
        }

        public void Navigate( Type type )
        {
            Navigate( type, null );
        }

        public void Navigate( Type type, object parameter )
        {
            if (CurrentLocation != null)
            {
                _navigationHistory.Push( CurrentLocation );
            }

            var args = new NavigationEventArgs( type, parameter );
            Navigate( args );
        }

        private void Navigate( NavigationEventArgs args )
        {
            OnNavigationRequested( args );

            CurrentLocation = args.TargetType;
        }

        private void OnNavigationRequested( NavigationEventArgs e )
        {
            NavigationRequested?.Invoke( this, e );
        }
    }
}