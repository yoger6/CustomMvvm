using System;

namespace CustomMvvm
{
    public class ApplicationException : Exception
    {
        public ApplicationException( string message )
            : base( message )
        {
        }
    }
}