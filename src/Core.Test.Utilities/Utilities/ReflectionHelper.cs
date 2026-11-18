using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Test.Utilities.Utilities
{
    public class ReflectionHelper
    {
        public static ConstructorInfo GetConstructor<T>()
        {
            return typeof(T).GetConstructors().First();
        }

        public static Type[] GetParameterTypes(
            ConstructorInfo constructor)
        {
            return constructor.GetParameters()
                .Select(p => p.ParameterType)
                .ToArray();
        }

        public static object InvokeGenericMethod(
            object instance, string methodName, params Type[] types)
        {
            var method = instance.GetType().GetMethod(methodName);
            var invoker = method.MakeGenericMethod(types);

            return invoker.Invoke(instance, null);
        }

        public static T Instantiate<T>(
            ConstructorInfo constructor, IEnumerable<object> arguments)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic |
                                       BindingFlags.CreateInstance | BindingFlags.OptionalParamBinding;

            return (T)constructor.Invoke(flags, null, arguments.ToArray(), null);
        }
    }
}