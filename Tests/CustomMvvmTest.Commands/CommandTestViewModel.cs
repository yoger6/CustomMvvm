using CustomMvvm;
using CustomMvvm.Commands;

namespace CustomMvvmTest.Commands
{
    internal class CommandTestViewModel : ViewModel
    {
        private int _id;

        public WatchingCommand<object> Command { get; }

        public int Id
        {
            private get => _id;
            set => Set( ref _id, value );
        }

        public WatchingCommand ParameterlessCommand { get; }

        public CommandTestViewModel( int desiredPropertyValue )
        {
            Command = new WatchingCommand<object>( o => { }, () => Id == desiredPropertyValue, this, nameof(Id) );
            ParameterlessCommand = new WatchingCommand( () => { }, () => Id == desiredPropertyValue, this, nameof(Id) );
        }
    }
}