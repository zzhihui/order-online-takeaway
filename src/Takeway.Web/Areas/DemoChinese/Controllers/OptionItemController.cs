using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoChinese.Controllers
{
    public class OptionItemController : BaseOptionItemController, IOptionItemController
    {
        public OptionItemController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }
    }
}