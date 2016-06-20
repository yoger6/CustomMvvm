using CustomMvvm;

namespace CustomMvvmTest
{
    internal class ValidationTestViewModel : ValidatableViewModel
    {
        private int _id;

        [CustomMvvm.Validation.Range( 0, 2 )]
        public int Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }
    }
}