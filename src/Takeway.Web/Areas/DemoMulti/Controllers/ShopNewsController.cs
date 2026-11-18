using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoMulti.Controllers
{

   


    public class ShopNewsController : BaseShopNewsController
    {
        public ShopNewsController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemoMulti);
        }


       
    }
}