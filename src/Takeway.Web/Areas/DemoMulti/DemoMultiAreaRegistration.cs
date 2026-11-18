using System.Web.Mvc;

namespace Takeway.Web.Areas.DemoMulti
{
    public class DemoMultiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DemoMulti";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                 "DemoMulti_default",
                 "DemoMulti/{controller}/{action}/{id}",
                 new { Controller = "Home", action = "Index", id = UrlParameter.Optional },
                 new[] { "Takeway.Web.Areas.DemoMulti.Controllers" }


             );
        }
    }
}