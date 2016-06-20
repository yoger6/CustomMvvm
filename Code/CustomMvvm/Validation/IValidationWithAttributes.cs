using System.Collections.Generic;
using System.ComponentModel;

namespace CustomMvvm.Validation
{
    public interface IValidationWithAttributes : INotifyDataErrorInfo
    {
        void RemoveErrors( string property );
        void SetErrors( string property, IEnumerable<string> errors );
    }
}