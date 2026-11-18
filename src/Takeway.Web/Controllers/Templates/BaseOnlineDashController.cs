using System.Web.Mvc;
using Takeaway.Service.Contract.Enum;
using Takeway.Web.Application.Filter;
using Takeway.Web.Models.OnlineDash;

namespace Takeway.Web.Controllers.Templates
{

    public interface IOnlineDashController
    {
        ActionResult Index();
        ActionResult Index(OnlineDashViewModel viewModel);
        ActionResult Reprint(int? id);

        ActionResult Cancel(int? id);
    }

    [AreaAuthorization]
    public class BaseOnlineDashController : BaseContoller
    {

        public virtual ActionResult Index()
        {
            var viewModel = new OnlineDashViewModel
            {
                ShopInfo = ShopService.GetShopInfo(ShopId),
                OrdersToday = ShopService.GetOpenOrders(ShopId)
            };

            if (viewModel.ShopInfo.IsOpenByOwner.HasValue && viewModel.ShopInfo.IsOpenByOwner.Value)
                viewModel.IsOpen = true;
            else
                viewModel.IsOpen = false;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Index(OnlineDashViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.ShopInfo.IsOpenByOwner = viewModel.IsOpen;
                ShopService.UpdateShopInfo(viewModel.ShopInfo, (int)WsUpdateShopInfoOption.UpdateOnlineEnabled, ShopId);

            }

            return RedirectToAction("Index");
        }

        
        public virtual ActionResult Reprint(int? id)
        {

            if (!id.HasValue || id.Value <= 0) return RedirectToAction("Index");

            var order = ShopService.GetOrderById(id.Value, ShopId);
            order = ShopService.UpdateOrder(order, (int)UpdateOrderOption.PrintOnlineOrder, ShopId);

            return RedirectToAction("Index");
        }

        public virtual ActionResult Cancel(int? id)
        {
            if (!id.HasValue || id.Value <= 0) return RedirectToAction("Index");

            var order = ShopService.GetOrderById(id.Value, ShopId);
            order = ShopService.CancelOrderById(order, ShopId);

            return RedirectToAction("Index");
        }
        
        public virtual ActionResult Detail(int? id)
        {

            if (!id.HasValue || id.Value <= 0) return RedirectToAction("Index");

            var viewModel = new OnlineDashViewModel { SelectedOrder = ShopService.GetOrderById(id.Value, ShopId) };

            return View(viewModel);
        }
    }
}