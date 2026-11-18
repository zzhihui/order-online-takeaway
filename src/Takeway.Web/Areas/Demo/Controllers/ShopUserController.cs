using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.Demo.Controllers
{
    public class ShopUserController : BaseShopUserController, IShopUserController
    {

        public ShopUserController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }


    }
}