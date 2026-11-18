using System.Linq;
using System.Web.Mvc;
using Takeaway.Service.Contract.Enum;
using Takeway.Web.Controllers.Templates;
using Takeway.Web.ViewModels.Order;

namespace Takeway.Web.Areas.DemoMulti.Controllers
{
    public class OrderController : BaseOrderController, IOrderController
    {
        
        public OrderController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemoMulti);
        }



        public override ActionResult Index()
        {
            return RedirectToAction("CategoryList");
        }

    }
}