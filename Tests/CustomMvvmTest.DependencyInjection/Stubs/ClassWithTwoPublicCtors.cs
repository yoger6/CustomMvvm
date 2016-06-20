
using CustomMvvm.DependencyInjection;

namespace CustomMvvmTest.DependencyInjection.Stubs
{
    internal class ClassWithTwoPublicCtors
    {
        public ClassWithPublicCtor ClassWithPublicCtor { get; set; }

        public ClassWithTwoPublicCtors()
        { }

        [PreferredConstructor]
        public ClassWithTwoPublicCtors( ClassWithPublicCtor classWithPublicCtor )
        {
            ClassWithPublicCtor = classWithPublicCtor;
        }
    }
}