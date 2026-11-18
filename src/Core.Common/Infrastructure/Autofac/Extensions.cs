using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using Castle.Core.Internal;
using Core.Common.Infrastructure.Configuration;

namespace Core.Common.Infrastructure.Autofac
{
    public static class Extensions
    {
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> FromThisAssembly(
            this ContainerBuilder builder)
        {
            return builder.RegisterAssemblyTypes(Assembly.GetCallingAssembly());
        }


        public static List<Assembly> GetApplicationAssemblies()
        {
            var assemblyConfigurations = (DependencyAssemblySection)ConfigurationManager.GetSection("DependencyAssemblySection");

            var listOfAssemblyNames = (from DependencyAssemblyElement element in assemblyConfigurations.Assemblies select element.AssemblyName).ToList();

            return BuildManager.GetReferencedAssemblies().Cast<Assembly>()
               .Where(
                        a => listOfAssemblyNames.Contains(a.GetName().Name)
                       ).ToList();
        }


        public static void RegisterProgramModules(
           this ContainerBuilder container, List<Assembly> assemblies)
        {
            var modules = assemblies.GetProgramModules();

            var configurator = new ProgramConfigurator(container);

            modules.ForEach(m => m.Configure(configurator));
        }


        private static List<IProgramModule> GetProgramModules(this IEnumerable<Assembly> assemblies)
        {
            var modules = new List<IProgramModule>();

            foreach (var assembly in assemblies)
                modules.AddRange(assembly.GetProgramModules());

            return modules;
        }


        private static IEnumerable<IProgramModule> GetProgramModules(this Assembly assembly)
        {
            return assembly.GetTypesImplementing<IProgramModule>()
                .Select(module => (IProgramModule)Activator.CreateInstance(module)).ToList();
        }

        private static IEnumerable<Type> GetTypesImplementing<T>(this Assembly assembly)
        {
            return assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(T))).ToList();
        }


        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>
           FromAssemblyContaining<T>(this ContainerBuilder builder)
        {
            return builder.RegisterAssemblyTypes(typeof(T).Assembly);
        }

        public static void ForEach<T>(this IContainer container, Action<T> action)
        {
            container.Resolve<IEnumerable<T>>().ForEach(action);
        }

        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> AnnotatedWith<T>(
           this IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> registration)
           where T : Attribute
        {
            return registration.Where(t => t.HasAttribute<T>());
        }

        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> WithDefaults(
          this IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> registration)
        {
            return registration.AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        public static IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> WithDefaults(
            this IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> registration)
        {
            return registration.AsImplementedInterfaces().InstancePerLifetimeScope();
        }


    }
}