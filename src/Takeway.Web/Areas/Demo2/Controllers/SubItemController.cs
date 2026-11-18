using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.Demo2.Controllers
{
    public class SubItemController : BaseSubItemController
    {
        public SubItemController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }
    }
}