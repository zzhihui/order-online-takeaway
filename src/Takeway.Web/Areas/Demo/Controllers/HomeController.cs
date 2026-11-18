using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.Demo.Controllers
{
    public class HomeController : BaseHomeController, IHomeController
    {
        public HomeController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }
        
    }
}