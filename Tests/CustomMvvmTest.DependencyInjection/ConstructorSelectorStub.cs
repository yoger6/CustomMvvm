using System;
using System.Reflection;
using CustomMvvm.DependencyInjection;

namespace CustomMvvmTest.DependencyInjection
{
    internal class ConstructorSelectorStub : IConstructorSelector
    {
        public ConstructorInfo GetConstructor<T>() where T : class
        {
            throw new NotImplementedException();
        }
    }
}