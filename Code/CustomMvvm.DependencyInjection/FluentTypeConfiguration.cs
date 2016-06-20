namespace CustomMvvm.DependencyInjection
{
    public class FluentTypeConfiguration
    {
        private readonly TypeConfiguration _configuration;

        public FluentTypeConfiguration( TypeConfiguration configuration )
        {
            _configuration = configuration;
        }

        public FluentTypeConfiguration For<T>()
        {
            _configuration.SupportedType = typeof( T );
            return this;
        }

        public FluentTypeConfiguration When<T>()
        {
            _configuration.InjectionTarget = typeof( T );
            return this;
        }

        public FluentTypeConfiguration AsSingleton()
        {
            _configuration.SingleInstance = true;
            return this;
        }
    }
}