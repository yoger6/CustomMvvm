using CustomMvvm;
using NUnit.Framework;

namespace CustomMvvmTest
{
    [TestFixture]
    public class ViewModelTest
    {
        [Test]
        public void IsAbstract()
        {
            Assert.IsTrue( typeof( ViewModel ).IsAbstract );
        }

        [Test]
        public void IsObservable()
        {
            Assert.True( typeof( ObservableObject ).IsAssignableFrom( typeof( ViewModel ) ) );
        }
    }
}