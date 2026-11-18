using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.Demo2.Controllers
{

   


    public class ShopNewsController : BaseShopNewsController
    {
        public ShopNewsController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }


       
    }
}