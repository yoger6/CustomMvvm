using System;
using System.Collections.Generic;

namespace CustomMvvm.Utilities
{
    public static class Extensions
    {
        public static void Each<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action.Invoke( item );
            }
        }
    }
}
