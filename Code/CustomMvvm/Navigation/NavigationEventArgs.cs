using System;

namespace CustomMvvm.Navigation
{
    public class NavigationEventArgs : EventArgs
    {
        public Type TargetType { get; }
        public object Parameter { get; }
        public bool HasParameter => Parameter != null;

        public NavigationEventArgs( Type targetType, object parameter = null )
        {
            TargetType = targetType;
            Parameter = parameter;
        }
    }
}