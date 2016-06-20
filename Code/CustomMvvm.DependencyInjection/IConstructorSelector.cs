using System.Reflection;

namespace CustomMvvm.DependencyInjection
{
    public interface IConstructorSelector
    {
        ConstructorInfo GetConstructor<T>() where T : class;
    }
}