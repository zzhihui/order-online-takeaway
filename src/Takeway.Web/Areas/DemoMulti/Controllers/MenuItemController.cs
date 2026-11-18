using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoMulti.Controllers
{
    public class MenuItemController : BaseMenuItemController, IMenuItemController
    {


        public MenuItemController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemoMulti);
        }




    }
}