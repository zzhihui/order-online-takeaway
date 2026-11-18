using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoChinese.Controllers
{
    public class DeliveryCostController : BaseDeliveryCostController
    {
        public DeliveryCostController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }
    }
}