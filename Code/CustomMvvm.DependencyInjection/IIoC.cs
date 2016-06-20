using System;

namespace CustomMvvm.DependencyInjection
{
    public interface IIoC
    {
        FluentTypeConfiguration ConfigureType<T>();
        void RegisterInstance<T>( T obj );
        T Resolve<T>() where T : class;
        object Resolve( Type type );
    }
}