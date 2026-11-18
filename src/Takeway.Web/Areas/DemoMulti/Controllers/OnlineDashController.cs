using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoMulti.Controllers
{
    public class OnlineDashController : BaseOnlineDashController, IOnlineDashController
    {

        public OnlineDashController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemoMulti);
        }

       

    }
}