using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoChinese.Controllers
{
    public class ShopUserController : BaseShopUserController, IShopUserController
    {
        public ShopUserController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }
    }
}