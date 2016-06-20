using System;
using CustomMvvm.Navigation;

namespace CustomMvvm
{
    public abstract class MainViewModelBase : ObservableObject
    {
        private ViewModel _currentViewModel;

        protected readonly INavigationService Navigation;

        public ViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            protected set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged();
                }
            }
        }


        protected MainViewModelBase( INavigationService navigation )
        {
            if (navigation == null)
            {
                throw new ArgumentNullException( nameof( navigation ) );
            }
            Navigation = navigation;
            Navigation.NavigationRequested += OnNavigationRequested;
        }

        private void OnNavigationRequested( object sender, NavigationEventArgs navigationEventArgs )
        {
            var instance = GetInstanceToNavigateTo( navigationEventArgs.TargetType );
            var navigable = _currentViewModel as INavigable;
            (CurrentViewModel as INavigable)?.OnNavigatedFrom();

            CurrentViewModel = instance;
            (CurrentViewModel as INavigable)?.OnNavigatedTo();
        }

        protected abstract ViewModel GetInstanceToNavigateTo( Type type );
    }
}