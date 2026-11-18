using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoMulti.Controllers
{
    public class ShopUserController : BaseShopUserController, IShopUserController
    {

        public ShopUserController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemoMulti);
        }


    }
}