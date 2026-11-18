using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.Demo2.Controllers
{
    public class OptionItemController : BaseOptionItemController, IOptionItemController
    {
        public OptionItemController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }

       

    }
}