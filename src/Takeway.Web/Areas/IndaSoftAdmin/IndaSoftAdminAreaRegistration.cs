using System.Web.Mvc;

namespace Takeway.Web.Areas.IndaSoftAdmin
{
    public class IndaSoftAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "IndaSoftAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "IndaSoftAdmin_default",
                "IndaSoftAdmin/{controller}/{action}/{id}",
                 new { Controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Takeway.Web.Areas.IndaSoftAdmin.Controllers" }
            );
        }
    }
}