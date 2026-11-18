using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoMulti.Controllers
{
    public class AdminController : BaseAdminController, IAdminController
    {

        public AdminController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemoMulti);
        }

        
    }
}