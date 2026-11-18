using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.Demo2.Controllers
{
    public class DeliveryCostController : BaseDeliveryCostController
    {
        public DeliveryCostController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }

    }
}