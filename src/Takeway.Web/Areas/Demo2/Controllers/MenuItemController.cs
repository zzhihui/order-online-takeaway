using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.Demo2.Controllers
{
    public class MenuItemController : BaseMenuItemController, IMenuItemController
    {


        public MenuItemController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }




    }
}