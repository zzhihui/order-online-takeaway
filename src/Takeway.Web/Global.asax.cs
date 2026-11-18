using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net.Config;
using Takeway.Web.App_Start;
using Takeway.Web.Infrastructure.Filter;

namespace Takeway.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Filters.Add(new IpHostValidationAttribute());
            GlobalConfiguration.Configuration.Filters.Add(new TokenValidationAttribute());
            AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;

            AutofacConfig.RegisterDependencies();

            #if DEBUG
            XmlConfigurator.Configure();
            #endif
        }
    }
}
