using System.Web.Mvc;

namespace Takeway.Web.Areas.DemoChinese
{
    public class DemoChineseAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DemoChinese";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DemoChinese_default",
                "DemoChinese/{controller}/{action}/{id}",
                 //TODO: Localisation
                 new { Controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Takeway.Web.Areas.DemoChinese.Controllers" }
            );
        }
    }
}