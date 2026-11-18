using System.Web.Mvc;
using Takeway.Web.Controllers.Templates;
using Takeway.Web.ViewModels.Order;

namespace Takeway.Web.Areas.Demo2.Controllers
{
    public class HomeController : BaseHomeController, IHomeController
    {
        public HomeController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }


        public override ActionResult Index()
        {
            return View();
        }

       
    }
}