using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Validation;
using FluentValidation;

namespace Core.Common
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

            builder.RegisterAssemblyTypes(thisAssembly)
               .Where(t => typeof(IUnitOfWork).IsAssignableFrom(t))
               .PropertiesAutowired()
               .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(thisAssembly)
                .Where(t => typeof(IUnitOfWorkContext).IsAssignableFrom(t))
                .PropertiesAutowired()
                .AsImplementedInterfaces().InstancePerLifetimeScope();


            AssemblyScanner.FindValidatorsInAssembly(thisAssembly)
               .ForEach(v => builder.RegisterType(v.ValidatorType).As(v.InterfaceType));

            builder.RegisterType<ValidatorFactory>().As<IValidatorFactory>();


        }

    }
}
