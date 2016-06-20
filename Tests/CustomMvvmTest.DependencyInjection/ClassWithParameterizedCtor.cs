using CustomMvvmTest.DependencyInjection.Stubs;

namespace CustomMvvmTest.DependencyInjection
{
    internal class ClassWithParameterizedCtor
    {
        public ClassWithParameterizedCtor( int number )
        {}

        public ClassWithParameterizedCtor(ClassWithPublicCtor withPublic, 
            ClassWithTwoPublicCtors withTwoPublic)
        {}
    }
}