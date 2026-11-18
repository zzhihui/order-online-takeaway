using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoChinese.Controllers
{
    public class ServerSettingController : BaseServerSettingController
    {
        public ServerSettingController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }
    }
}