using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.Demo2.Controllers
{
    public class ServerSettingController : BaseServerSettingController
    {
        public ServerSettingController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }
    }
}