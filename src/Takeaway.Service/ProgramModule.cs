using System.Reflection;
using Autofac;
using Core.Common.Infrastructure.Autofac;

namespace Takeaway.Service
{
    public class ProgramModule : IProgramModule
    {
        public void Configure(IProgramConfigurator configuration)
        {
            configuration.Build(Build);
        }

        private void Build(ContainerBuilder builder)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(thisAssembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.FromThisAssembly().AnnotatedWith<TransformerAttribute>().WithDefaults();


        }

    }
}