using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoChinese.Controllers
{
    public class AdminController : BaseAdminController, IAdminController
    {
        public AdminController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }
    }
}