using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoChinese.Controllers
{
    public class MenuItemController : BaseMenuItemController, IMenuItemController
    {
        public MenuItemController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }
    }
}