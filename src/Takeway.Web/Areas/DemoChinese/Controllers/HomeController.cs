using System.Web.Mvc;
using System.Web.UI;
using Takeway.Web.Controllers.Templates;
using Takeway.Web.ViewModels.Order;

namespace Takeway.Web.Areas.DemoChinese.Controllers
{
    public class HomeController : BaseHomeController, IHomeController
    {
        public HomeController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }

        [OutputCache(Duration = 600, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public override ActionResult Index()
        {
            var viewModel = new OrderViewModel
            {
                ShopInfo = ShopService.GetShopInfo(ShopId)
            };

            if (viewModel.ShopInfo == null)
                return Redirect("http://www.kingfood.ie");

            return View(viewModel);
        }

        public override ActionResult App()
        {

            var viewModel = new OrderViewModel
            {
                ShopInfo = ShopService.GetShopInfo(ShopId),
            };

            if (viewModel.ShopInfo == null)
                return Redirect("http://www.kingfood.ie");

            var agent = Request.UserAgent.ToLower();

            // Check if IOS
            if (agent.Contains("iphone"))
            {
                return Redirect(viewModel.ShopInfo.IoSlink);
            }
            if (agent.Contains("android"))
            {
                return Redirect(viewModel.ShopInfo.Androidlink);
            }
            return RedirectToAction("Index");


        }

    }
}