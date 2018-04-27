using CustomMvvm;

namespace CustomMvvmTest
{
    internal class TestEntityViewModel : EntityViewModel<TestEntity>
    {
        private int _id;

        public int Id
        {
            get => _id;
            set => Set( ref _id, value );
        }

        public TestEntityViewModel( TestEntity model ) : base( model )
        {
        }

        protected override void SaveChangesToModel()
        {
            Model.Id = Id;
        }

        protected override bool VerifyModelForChanges()
        {
            return _id != Model.Id;
        }
    }
}