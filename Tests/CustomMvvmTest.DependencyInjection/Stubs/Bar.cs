namespace CustomMvvmTest.DependencyInjection.Stubs
{
    internal class Bar : IBar{}

    internal class AnotherBar : IBar {}

    internal class Foo
    {
        public IBar Bar { get; }

        public Foo(IBar bar)
        {
            Bar = bar;
        }
    }
}