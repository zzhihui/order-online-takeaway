using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoMulti.Controllers
{
    public class SubItemController : BaseSubItemController
    {
        public SubItemController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemoMulti);
        }
    }
}