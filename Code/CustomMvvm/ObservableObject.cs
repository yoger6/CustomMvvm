using System.ComponentModel;
using System.Runtime.CompilerServices;
using CustomMvvm.Annotations;

namespace CustomMvvm
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool Set<T>( ref T field, T value, [CallerMemberName] string propertyName = null )
        {
            if ( ShouldKeepTheOldValue( field, value ) )
            {
                return false;
            }

            field = value;
            OnPropertyChanged( propertyName );
            return true;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged( string propertyName )
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }

        private static bool ShouldKeepTheOldValue<T>( T field, T value )
        {
            return field != null && field.Equals( value ) || field == null && value == null;
        }
    }
}