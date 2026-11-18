using System.Web.Mvc;

namespace Takeway.Web.Areas.Demo2
{
    public class Demo2AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Demo2";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Demo2_default",
                "Demo2/{controller}/{action}/{id}",
                 new { Controller = "Home", action = "Index", id = UrlParameter.Optional },
                 new[] { "Takeway.Web.Areas.Demo2.Controllers" }
            );
        }
    }
}