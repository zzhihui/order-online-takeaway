using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoMulti.Controllers
{
    public class OptionItemController : BaseOptionItemController, IOptionItemController
    {
        public OptionItemController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemoMulti);
        }

       

    }
}