using AthenaBackend.Common.DomainDrivenDesign;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AthenaBackend.Common.DependecyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, Assembly interfaceAssembly, Assembly classAssembly)
            => services.AddInterfaceOfInterface<IRepository>(interfaceAssembly,
                                                                classAssembly,
                                                                new List<Type> {
                                                                    typeof(IRepository),
                                                                    typeof(IRepository<,>)
                                                                });

        public static IServiceCollection AddInterfaceOfInterface<T>(this IServiceCollection services,
                                                                    Assembly interfaceAssembly,
                                                                    Assembly classAssembly,
                                                                    List<Type> toExclude = null)
            => services.AddInterfaceOfInterface(interfaceAssembly, classAssembly, typeof(T), toExclude);

        public static IServiceCollection AddInterfaceOfInterface(this IServiceCollection services,
                                                                    Assembly interfaceAssembly,
                                                                    Assembly classAssembly,
                                                                    Type type,
                                                                    List<Type> toExclude = null)
        {
            var interfaceTypes = interfaceAssembly.GetInterfacesInheritedFrom(type, toExclude);

            foreach (var interfaceType in interfaceTypes)
            {
                var classType = classAssembly.GetImplementationsOf(interfaceType).FirstOrDefault();

                if (classType == null)
                {
                    throw new InvalidOperationException($"There aren't implementation of {interfaceType}");
                }

                services.AddScoped(interfaceType, classType);
            }

            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services, Assembly interfaceAssembly)
        {
            var interfaceTypes = interfaceAssembly.GetImplementationsOf<IService>();

            foreach (var interfaceType in interfaceTypes)
            {
                services.AddScoped(interfaceType, interfaceType);
            }

            return services;
        }

        public static IServiceCollection AddConverters(this IServiceCollection services, Assembly interfaceAssembly)
        {
            var convertersTypes = interfaceAssembly.GetClassesImplementingIConverter();

            foreach (var convertersType in convertersTypes)
            {
                var interfaceType = convertersType.GetInterfaces().FirstOrDefault();
                services.AddScoped(interfaceType, convertersType);
            }

            return services;
        }

    }
}
