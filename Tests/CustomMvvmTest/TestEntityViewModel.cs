using CustomMvvm;

namespace CustomMvvmTest
{
    internal class TestEntityViewModel : EntityViewModel<TestEntity>
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
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