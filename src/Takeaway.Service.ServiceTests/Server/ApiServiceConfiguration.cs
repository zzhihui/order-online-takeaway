//using System.Web.Http;
//using Autofac;
//using Autofac.Integration.WebApi;
//using Newtonsoft.Json.Serialization;
//using Takeaway.BusinessEngine.BusinessEngine;

//using Takeaway.Service.Services;
//using Takeway.Web.Controllers.Api;

//namespace Takeaway.Service.ServiceTests.Server
//{
//    public interface IApiApplication
//    {
//        void Configure();
//    }
//    public class ApiServiceConfiguration : IApiApplication
//    {
//        private readonly HttpConfiguration _configuration;

//        public ApiServiceConfiguration(HttpConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public void Configure()
//        {
//            ConfigureRoutes();
//            ConfigureDependencies();
//        }

//        private void ConfigureDependencies()
//        {
//            var builder = new ContainerBuilder();
//            builder.RegisterHttpRequestMessage(_configuration);
//            builder.Register(c => _configuration.Routes).As<HttpRouteCollection>();



//            builder.RegisterAssemblyTypes(typeof(AuthenticationService).Assembly)
//                .AsImplementedInterfaces()
//                .InstancePerLifetimeScope();


//            builder.RegisterAssemblyTypes(typeof(AuthenticationBusinessEngine).Assembly)
//                .AsImplementedInterfaces()
//                .InstancePerLifetimeScope();


//            //builder.RegisterApiControllers(typeof(AuthenticationApiController).Assembly);
//            //var container = builder.Build();
//            //var resolver = new AutofacWebApiDependencyResolver(container);
//            //_configuration.DependencyResolver = resolver;
//        }

//        private void ConfigureRoutes()
//        {
//            // Use camel case for JSON data.
//            _configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

//            // Web API routes
//            _configuration.MapHttpAttributeRoutes();

//            _configuration.Routes.MapHttpRoute(
//                name: "DefaultApi",
//                routeTemplate: "api/{controller}/{action}/{id}",
//                defaults: new { id = RouteParameter.Optional }
//            );
//        }
//    }
//}