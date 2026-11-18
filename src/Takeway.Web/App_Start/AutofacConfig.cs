using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Service.Transformers;
using Takeway.Web.Controllers.Api;


namespace Takeway.Web.App_Start
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var assemblies = Extensions.GetApplicationAssemblies();
            ConfigureDependencies(assemblies);
        }

        private static void ConfigureDependencies(List<Assembly> assemblies)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(t => typeof(IApiControllerBase).IsAssignableFrom(t))
            .PropertiesAutowired()
            .AsSelf();


            builder.RegisterProgramModules(assemblies);

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            container.ForEach<ITransformerMapper>(t => t.Initialise());

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
             

            ApplicationScope.Container = container;
        }
    }
}