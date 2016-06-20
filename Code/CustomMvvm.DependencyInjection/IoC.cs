using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CustomMvvm.DependencyInjection
{
    public class IoC : IIoC
    {
        protected static IoC instance;
        protected readonly Dictionary<Type, object> Instances;
        protected readonly ICollection<TypeConfiguration> Configurations;
        protected IConstructorSelector ConstructorSelector = new ConstructorSelector();

        public static IoC Instance
        {
            get {
                if (instance == null)
                {
                    instance = new IoC();
                }
                return instance;
            }
        }

        protected IoC()
        {
            Instances = new Dictionary<Type, object>();
            Configurations = new List<TypeConfiguration>();

            if (instance == null)
            {
                instance = this;
            }
        }

        public FluentTypeConfiguration ConfigureType<T>()
        {
            var config = new TypeConfiguration( typeof(T) );
            Configurations.Add( config );

            return new FluentTypeConfiguration( config );
        }
        
        public T Resolve<T>() where T : class
        {
            if (typeof(T) == typeof(IIoC) || typeof( T ) == typeof(IoC))
            {
                return this as T;
            }

            if (IsSingleton( typeof(T) ))
            {
                return ActivateSingleton<T>();
            }
            if (HasSupportingType( typeof(T) ))
            {
                return ActivateSupportingType<T>();
            }
            
            return  ActivateInstance<T>();
        }
        public object Resolve( Type type )
        {
            var method = GetType().GetRuntimeMethod( "Resolve", new Type[] { } );
            var generic = method.MakeGenericMethod( type );

            return generic.Invoke( this, null );
        }


        public void RegisterInstance<T>( T obj )
        {
            if (CanRegisterInstance( typeof(T) ))
            {
                Instances.Add( typeof(T), obj );
            }
            else
            {
                throw new InvalidOperationException(
                    $"Instance of type {typeof(T)} cannot be registered as it is not configured as Singleton" );
            }
        }

        private bool CanRegisterInstance( Type type )
        {
            var config = Configurations.FirstOrDefault( x => x.ConfiguredType == type )?.SingleInstance;

            return config.HasValue && config.Value;
        }

        private bool HasSupportingType( Type type )
        {
            var confg = Configurations.FirstOrDefault( x => x.IsSupporting( type ) );

            return confg != null;
        }
        private T ActivateSupportingType<T>() where T : class
        {
            var supportingType = Configurations.FirstOrDefault( x => x.IsSupporting( typeof(T) ) ).ConfiguredType;

            return Resolve( supportingType ) as T;
        }
        
        private bool IsSingleton( Type type )
        {
            var config = Configurations.FirstOrDefault( x => x.ConfiguredType == type );

            return config != null && config.SingleInstance;
        }
        private T ActivateSingleton<T>() where T : class
        {
            var type = typeof(T);
            if (!Instances.ContainsKey( type ))
            {
                Instances.Add( type, ActivateInstance<T>() );
            }

            return Instances[type] as T;
        }

        private T ActivateInstance<T>() where T : class
        {
            var constructor = ConstructorSelector.GetConstructor<T>(); 
            var parameters = constructor.GetParameters();
            if (parameters.Length == 0)
            {
                return Activator.CreateInstance<T>();
            }
            
            var list = new List<object>( parameters.Select( parameterInfo => ResolveParameter( typeof( T ), parameterInfo.ParameterType ) ) );

            var instance = constructor.Invoke(list.ToArray());

            return (T) instance;
        }

        private object ResolveParameter( Type constructorOwner, Type parameterType )
        {
            var type = GetPreferredImplementation( constructorOwner, parameterType );

            return  Resolve( type );
        }

        private Type GetPreferredImplementation( Type constructorOwner, Type parameterType )
        {
            var config = Configurations.FirstOrDefault( x => x.InjectionTarget == constructorOwner && x.IsSupporting( parameterType ) );

            return config?.ConfiguredType ?? parameterType;
        }

        public void SetConstructorSelector( IConstructorSelector selector )
        {
            if (selector != null)
            {
                ConstructorSelector = selector;
            }
        }
    }
}
