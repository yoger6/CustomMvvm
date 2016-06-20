using System;
using System.Collections.Generic;
using CustomMvvm.DependencyInjection;

namespace CustomMvvmTest.DependencyInjection
{
    public class IoCShim : IoC
    {
        public ICollection<TypeConfiguration> ExposedConfigurations => Configurations;
        public Dictionary<Type, object> ExposedInstances => Instances;
        public IConstructorSelector ExposedConstructorSelector => ConstructorSelector;

        public void CleanupIoC()
        {
            Instances.Clear();
            Configurations.Clear();
            instance = null;
        }
    }
}