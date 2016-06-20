using System;
using System.Collections.Generic;

namespace CustomMvvm.Utilities
{
    public static class Log
    {
        private static readonly List<string> _logs;

        static Log()
        {
            _logs = new List<string>();
        }

        public static void Write( string message )
        {
            _logs.Add( $"{DateTime.Now} - {message}" );
        }

        public static List<string> GetAll()
        {
            return _logs;
        }
    }
}
