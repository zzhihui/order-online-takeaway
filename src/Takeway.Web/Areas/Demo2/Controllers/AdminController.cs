using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.Demo2.Controllers
{
    public class AdminController : BaseAdminController, IAdminController
    {

        public AdminController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }

        
    }
}