using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoMulti.Controllers
{
    public class DeliveryCostController : BaseDeliveryCostController
    {
        public DeliveryCostController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemoMulti);
        }

    }
}