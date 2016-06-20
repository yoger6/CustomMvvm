using System;
using System.Linq;
using System.Reflection;

namespace CustomMvvm.DependencyInjection
{
    public class ConstructorSelector : IConstructorSelector
    {
        public ConstructorInfo GetConstructor<T>() where T : class 
        {
            var constructors = GetPublicConstructors<T>();
            if (!constructors.Any())
            {
                throw new MissingMemberException($"{typeof(T)} doesn't contain any public constructors.");
            }
            
            return SelectPreferredCtor( constructors );
        }

        private ConstructorInfo SelectPreferredCtor( ConstructorInfo[] constructors )
        {
            var preferredCtor = GetCtorWithAttribute( constructors );

            if ( preferredCtor == null)
            {
                preferredCtor = GetFirstParameterlessCtor( constructors );
            }
            if (preferredCtor == null)
            {
                preferredCtor = GetFirstWithReferenceTypeParams( constructors );
            }

            return preferredCtor;
        }

        private ConstructorInfo GetFirstWithReferenceTypeParams( ConstructorInfo[] constructors )
        {
            return constructors.FirstOrDefault( x => x.GetParameters().Any( p => !p.ParameterType.GetTypeInfo().IsValueType ) );
        }

        private ConstructorInfo GetFirstParameterlessCtor( ConstructorInfo[] constructors )
        {
            return constructors.FirstOrDefault( x => !x.GetParameters().Any() );
        }

        private ConstructorInfo GetCtorWithAttribute( ConstructorInfo[] constructors )
        {
            return constructors
                .FirstOrDefault(x=>x.CustomAttributes
                    .Any( a => a.AttributeType == typeof( PreferredConstructorAttribute ) ) );
        }

        private ConstructorInfo[] GetPublicConstructors<T>()
        {
            return typeof (T).GetTypeInfo()
                             .DeclaredConstructors
                             .Where( x=>x.IsPublic )
                             .ToArray();
        }
    }
}