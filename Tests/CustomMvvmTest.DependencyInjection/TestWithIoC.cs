using NUnit.Framework;

namespace CustomMvvmTest.DependencyInjection
{
    [TestFixture]
    public abstract class TestWithIoC
    {
        protected IoCShim Container;

        [SetUp]
        public virtual void Initialize()
        {
            Container = new IoCShim();
        }
        
        [TearDown]
        public virtual void CleanUp()
        {
            Container.CleanupIoC();
        }
    }
}