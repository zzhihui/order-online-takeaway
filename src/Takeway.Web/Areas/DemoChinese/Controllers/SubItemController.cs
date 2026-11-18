using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoChinese.Controllers
{
    public class SubItemController : BaseSubItemController
    {
        public SubItemController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }
    }
}