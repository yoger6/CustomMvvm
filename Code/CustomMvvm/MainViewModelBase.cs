using System;
using CustomMvvm.Navigation;

namespace CustomMvvm
{
    public abstract class MainViewModelBase : ObservableObject
    {
        protected readonly INavigationService Navigation;
        private ViewModel _currentViewModel;

        public ViewModel CurrentViewModel
        {
            get => _currentViewModel;
            protected set => Set( ref _currentViewModel, value );
        }

        protected MainViewModelBase( INavigationService navigation )
        {
            if ( navigation == null )
            {
                throw new ArgumentNullException( nameof(navigation) );
            }

            Navigation = navigation;
            Navigation.NavigationRequested += OnNavigationRequested;
        }

        protected abstract ViewModel GetInstanceToNavigateTo( Type type );

        private void OnNavigationRequested( object sender, NavigationEventArgs navigationEventArgs )
        {
            var instance = GetInstanceToNavigateTo( navigationEventArgs.TargetType );
            ( CurrentViewModel as INavigable )?.OnNavigatedFrom();

            CurrentViewModel = instance;
            ( CurrentViewModel as INavigable )?.OnNavigatedTo();
        }
    }
}