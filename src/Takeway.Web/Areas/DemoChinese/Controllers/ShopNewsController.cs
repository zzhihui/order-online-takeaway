using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoChinese.Controllers
{
    public class ShopNewsController : BaseShopNewsController
    {
        public ShopNewsController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }
    }
}