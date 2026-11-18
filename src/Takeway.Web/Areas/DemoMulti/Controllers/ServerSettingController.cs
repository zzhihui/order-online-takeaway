using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoMulti.Controllers
{
    public class ServerSettingController : BaseServerSettingController
    {
        public ServerSettingController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemoMulti);
        }
    }
}