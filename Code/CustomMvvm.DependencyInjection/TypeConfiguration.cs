using System;

namespace CustomMvvm.DependencyInjection
{
    public class TypeConfiguration
    {
        public Type ConfiguredType { get; set; }
        public Type SupportedType { get; set; }
        public Type InjectionTarget { get; set; }
        public bool SingleInstance { get; set; }
        
        public TypeConfiguration( Type type )
        {
            ConfiguredType = type;
        }
        
        public bool IsSupporting( Type typeToSupport )
        {
            return typeToSupport == SupportedType;
        }
    }
}
