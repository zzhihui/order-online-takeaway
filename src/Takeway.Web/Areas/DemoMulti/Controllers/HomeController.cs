using System.Web.Mvc;
using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoMulti.Controllers
{
    public class HomeController : BaseHomeController, IHomeController
    {
        public HomeController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemoMulti);
        }

        public override ActionResult Index()
        {
            return RedirectToAction("Home");
        }

    }
}