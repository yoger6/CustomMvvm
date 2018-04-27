using CustomMvvm;
using CustomMvvm.Validation;

namespace CustomMvvmTest
{
    internal class ValidationTestViewModel : ValidatableViewModel
    {
        private int _id;

        [Range( 0, 2 )]
        public int Id
        {
            get => _id;
            set => Set( ref _id, value );
        }
    }
}