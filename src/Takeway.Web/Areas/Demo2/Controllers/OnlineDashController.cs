using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.Demo2.Controllers
{
    public class OnlineDashController : BaseOnlineDashController, IOnlineDashController
    {

        public OnlineDashController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }


        //public override ActionResult Reprint(int? id)
        //{

        //    if (!id.HasValue || id.Value <= 0) return RedirectToAction("Index");


        //    var viewModel = new OnlineDashViewModel
        //    {
        //        ShopInfo = ShopService.GetShopInfo(ShopId),
        //        SelectedOrder = ShopService.GetOrderById(id.Value, ShopId)
        //    };

        //    return View(viewModel);
        //}



    }
}