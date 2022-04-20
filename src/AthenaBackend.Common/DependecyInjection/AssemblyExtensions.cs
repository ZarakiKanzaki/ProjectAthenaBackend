using AthenaBackend.Common.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AthenaBackend.Common.DependecyInjection
{
    public static class AssemblyExtensions
    {
        public static List<Type> GetImplementationsOf<T>(this Assembly thisAssembly) 
            =>  thisAssembly.GetImplementationsOf(typeof(T));

        public static List<Type> GetImplementationsOf(this Assembly thisAssembly, Type type) 
            => thisAssembly
                    .GetTypes()
                    .Where(p => type.IsAssignableFrom(p) 
                                && p.IsInterface == false 
                                && p != type)
                    .ToList();

        public static List<Type> GetInterfacesInheritedFrom<T>(this Assembly thisAssembly) 
            =>  thisAssembly.GetInterfacesInheritedFrom(typeof(T));

        public static List<Type> GetInterfacesInheritedFrom(this Assembly thisAssembly, Type type, List<Type> toExclude = null) 
            =>  thisAssembly
                    .GetTypes()
                    .Where(p => type.IsAssignableFrom(p) && p.IsInterface 
                                && (toExclude == null || toExclude.Contains(p) == false) 
                                && p != type)
                    .ToList();

        public static List<Type> GetClassesImplementingIConverter(this Assembly thisAssembly)
            => thisAssembly.GetTypes()
                           .Where(p =>  p.GetInterfaces().Any(i => i.Name == typeof(IConverter<,>).Name))
                           .ToList();

    }
}
