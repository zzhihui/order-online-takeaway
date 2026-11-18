using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.Demo.Controllers
{
    public class AdminController : BaseAdminController, IAdminController
    {

        public AdminController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }

        
    }
}