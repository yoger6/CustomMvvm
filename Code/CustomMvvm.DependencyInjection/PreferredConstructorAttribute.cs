using System;

namespace CustomMvvm.DependencyInjection
{
    [AttributeUsage( AttributeTargets.Constructor)]
    public class PreferredConstructorAttribute : Attribute
    {}
}