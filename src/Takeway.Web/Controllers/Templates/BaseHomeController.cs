using System.Web.Mvc;
using Takeway.Web.ViewModels.Order;

namespace Takeway.Web.Controllers.Templates
{

    public interface IHomeController
    {
        ActionResult Index();

    }

    public class BaseHomeController : BaseContoller
    {
        public virtual ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return RedirectToAction("Index", "Order");
        }


        public virtual ActionResult Home()
        {

            var viewModel = new OrderViewModel
            {
                ShopInfo = ShopService.GetShopInfo(ShopId),
            };

            if (viewModel.ShopInfo == null)
                return Redirect("http://www.kingfood.ie");

            return View(viewModel);
        }

        public virtual ActionResult App()
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